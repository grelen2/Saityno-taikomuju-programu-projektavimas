using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using RestGreta.Data.Entities;
using RestGreta.Data.Options;
using RestGreta.Data.Repositories; 


namespace RestGreta.Controllers
{
    [Route("api/authController")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IOptions<AuthOptions> options;

        internal MongoDBContext db = new MongoDBContext();

        public AuthController(IOptions<AuthOptions> auth)
        {
            this.options = auth;
        }
        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public IActionResult Login([FromBody] UserInfo request)
        {
            var user = AuthenticateUser(request.Email, request.Password);

            if (user != null)
            {

                //Generate JWT
                var token = GenerateJWT(user);

                return Ok(new
                {
                    access_token = token
                });
            }
            else
            return Unauthorized();
        }

        private User AuthenticateUser(string email, string pass)
        {
            var fil = Builders<User>.Filter.Eq(x => x.Email, email);
            var fi = Builders<User>.Filter.Eq(x => x.Password, pass);
            return db.User.Find(fil & fi).FirstOrDefault();
        }

        private string GenerateJWT(User user)
        {
            var authParams = options.Value;

            var securitKey = authParams.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securitKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(ClaimTypes.Role, user.Role)
            };

            // claims.Add(new Claim("Roles", user.Role));
            //foreach (var role in user.Role)
            //{
            //    claims.Add(new Claim("Role", role.ToString()));
            //}

            var token = new JwtSecurityToken(authParams.Issuer,
                authParams.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(authParams.TokenLifeTime),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
