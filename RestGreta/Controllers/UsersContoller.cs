using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestGreta.Data.Entities;
using RestGreta.Data.Repositories;
using RestGreta.Data.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestGreta.Controllers
{
    [Route("api/users")]
    [ApiController]

    public class UsersContoller : ControllerBase
    {
        IUserRepository db = new UserRepository();

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var userlist = await db.GetAllUsers();
            if (userlist == null)
            {
                return NotFound();
            }
            return Ok(userlist);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet(template: "{id}")]
        public async Task<ActionResult<User>> GetUser(string id)
        {
            var userlist = await db.GetUser(id);
            if (userlist == null)
            {
                return NotFound();
            }
            return Ok(userlist);
        }
        /*[Authorize(Roles = "Admin")]
        [HttpGet(template: "{email}")]*/
        public async Task<ActionResult<User>> GetUserEmail(string email)
        {
            var userlist = await db.GetUserEmail(email);
            if (userlist == null)
            {
                return NotFound();
            }
            return Ok(userlist);
        }
        [AllowAnonymous]
        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] UserCredentials cred)
        {
            User user = new User();
            
            if (cred == null)
            {
                return NotFound();
            }

            var el = await db.GetUserEmail(cred.Email);
            if (el != null)
            {

                return BadRequest("User with this email already exist");
            }
            else
            {

                    user.Email = cred.Email;
                    user.Password = cred.Password;
                    user.UserName = cred.Username;
                    user.Surname = cred.Surname;
                    user.Name = cred.Name;
                    user.Address = cred.Address;
                    if (cred.Role == "Admin")
                    {
                        user.Role = "Admin";
                    }
                    else
                    {
                        user.Role = "User";
                    }
                    await db.CreatUser(user);
                    return Created("Created", "New user created");
            }
            
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] User user)
        {
            if ( user == null)
            {
                return BadRequest();
            }
            if (user.Email == null || user.UserName == null || user.Name == null || user.Surname == null || user.Password == null)
            {
                return BadRequest("User email or/and name or/and name or/and surname or/and password souldn't be empty");
            }
            var el = await db.GetUserEmail(user.Email);
            if (el != null)
            {

                return BadRequest("User with this email already exist");
            }
            else
            {
                await db.CreatUser(user);
                return Created("Created", "Created");
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPut(template: "{id}")]
        public async Task<IActionResult> PutUser([FromBody] User user, string id)
        {
            var usr = await db.GetUser(id);
            if (usr == null)
            {
                return NotFound("User with this id not found");
            }
            if (user.UserName == null || user.Name == null || user.Surname == null || user.Password == null)
            {
                return BadRequest("User name or/and name or/and surname or/and password souldn't be empty");
            }
            else
            {
                user.Id = new string(id);
                await db.PutUser(user);
                return Ok();
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete(template: "{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var userlist = await db.GetUser(id);
            if (userlist == null)
            {
                return NotFound();
            }
            else
            {
                await db.DeleteUser(id);
                return NoContent();
            }
        }
        //------------------------------------------------

       [Authorize]
       [HttpDelete("{id}/recipe/{recipeId}/comment/{commentId}")]
        public async Task<IActionResult> DeleteComment(string id, string recipeId, string commentId)
        {
            var recipe = await db.GetUser(id);
            var comm = await db.GetRecipe(id, recipeId);
            var x = await db.GetRecipeComment(id, recipeId, commentId);
            if (recipe == null || comm == null || x == null)
            {
                return NotFound();
            }
            await db.DeleteRecipeComment(id, recipeId, commentId);
            return NoContent();
        }
        // DELETE api/<Users>/5
       [Authorize(Roles = "Admin")]
       [HttpDelete("{id}/products/{productId}")]
        public async Task<IActionResult> DeleteProduct(string id, string productId)
        {
            var product = await db.GetUser(id);
            var comm = await db.GetProduct(id, productId);
            if (product == null || comm == null)
            {
                return NotFound();
            }
            await db.DeleteProduct(id, productId);
            return NoContent();
        }
        // DELETE api/<Users>/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}/recipe/{recipeId}")]
        public async Task<IActionResult> DeleteRecipe(string id, string recipeId)
        {
            var recipe = await db.GetUser(id);
            var comm = await db.GetRecipe(id, recipeId);
            if (recipe == null || comm == null)
            {
                return NotFound();
            }
            await db.DeleteRecipe(id, recipeId);
            return NoContent();
        }
        // PUT api/<Users>/5
        [Authorize]
        [HttpPut("{id}/recipe/{recipeId}/comment/{commentId}")]
        public async Task<IActionResult> UpdateComment([FromBody] Comment recipe, string id, string recipeId, string commentId)
        {
            if (recipe== null)
            {
                return NotFound();
            }
            recipe.Id = commentId;
            await db.UpdateRecipeComment(recipe, id, recipeId, commentId);
            return Ok("Comment updated");
        }

        // PUT api/<Users>/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/recipe/{recipeId}")]
        public async Task<IActionResult> UpdateRecipe([FromBody] Recipe recipe, string id, string recipeId)
        {
            if (recipe == null)
            {
                return NotFound();
            }
            recipe.Id = new string(recipeId);
            await db.UpdateRecipe(recipe, id, recipeId);
            return Ok("Recipe updated");
        }
        // PUT api/<Users>/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/products/{productId}")]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product, string id, string productId)
        {
            if (product == null)
            {
                return NotFound();
            }
            product.Id = new string(productId);
            await db.UpdateProduct(product, id, productId);
            return Ok("Product updated");
        }
        // POST api/<Users>
        [Authorize]
        [HttpPost("{id}/recipe/{recipeId}/comment")]
        public async Task<IActionResult> CreateComment([FromBody] Comment comment, string id, string recipeId)
        {
            if (comment == null)
            {
                return NotFound();
            }
            if (comment.CommentText == string.Empty)
            {
                ModelState.AddModelError("Body", "The body shouldn't be empty");
            }
            await db.CreateRecipeComment(comment, id, recipeId);
            return Created("Created", true);
        }
        // POST api/<Users>
       [Authorize(Roles = "Admin")]
        [HttpPost("{id}/recipe")]
        public async Task<IActionResult> CreateRecipe([FromBody] Recipe recipe, string id)
        {
            if (recipe == null)
            {
                return NotFound();
            }
            if (recipe.Description == string.Empty)
            {
                ModelState.AddModelError("Body", "The body shouldn't be empty");
            }
            await db.CreateRecipe(recipe, id);
            return Created("Created", true);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("{id}/products")]
        public async Task<IActionResult> CreateProduct([FromBody] Product product, string id)
        {
            if (product == null)
            {
                return NotFound();
            }
            if (product.Name == string.Empty)
            {
                ModelState.AddModelError("Body", "The body shouldn't be empty");
            }
            await db.CreateProduct(product, id);
            return Created("Created", true);
        }

        [AllowAnonymous]
        [HttpGet("{id}/recipe/{recipeId}/comment")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetAllComments(string id, string recipeId)
        {
            var recipe = await db.GetUser(id);
            if (recipe == null)
            {
                return NotFound();
            }
            var all = await db.GetRecipeComments(id, recipeId);
            if (all == null)
            {
                return NotFound();
            }
            return Ok(all);
        }
        [AllowAnonymous]
        // GET api/<Users>/5
        [HttpGet("{id}/recipe/{recipeId}/comment/{commentId}")]
        public async Task<ActionResult<Comment>> GetUserComment(string id, string recipeId, string commentId)
        {
            var recipe = await db.GetRecipeComment(id, recipeId, commentId);
            if (recipe == null)
            {
                return NotFound();
            }
            return Ok(recipe);
        }

       [AllowAnonymous]
        [HttpGet("{id}/recipe")]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetAllRecipes(string id)
        {
            var recipe = await db.GetUser(id);
            if (recipe == null)
            {
                return NotFound();
            }
            var allrecipes = await db.GetRecipes(id);
            if (allrecipes == null)
            {
                return NotFound();
            }
            return Ok(allrecipes);
        }
       [AllowAnonymous]
        // GET api/<Users>/5
        [HttpGet("{id}/recipe/{recipeId}")]
        public async Task<ActionResult<User>> GetUserRecipes(string id, string recipeId)
        {
            var recipe = await db.GetRecipe(id, recipeId);
            if (recipe == null)
            {
                return NotFound();
            }
            return Ok(recipe);
        }

        [AllowAnonymous]
        [HttpGet("{id}/products")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts(string id)
        {
            var product = await db.GetUser(id);
            if (product == null)
            {
                return NotFound("nerastas naudotojas");
            }
            var allproducts = await db.GetProducts(id);
            if (allproducts == null)
            {
                return NotFound("Nerastas produktas");
            }
            return Ok(allproducts);
        }
        [AllowAnonymous]
        // GET api/<Users>/5
        [HttpGet("{id}/products/{productId}")]
        public async Task<ActionResult<User>> GetUserProducts(string id, string productId)
        {
            var product = await db.GetProduct(id, productId);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
    }
}
