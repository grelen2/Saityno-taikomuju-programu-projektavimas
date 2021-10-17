using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestGreta.Data.Entities;
using RestGreta.Data.Repositories;


namespace RestGreta.Controllers
{
    [Route("api/userList")]
    [ApiController]
    
    public class UserListContoller: ControllerBase
    {
        IUserListRepository db = new UserListRepository();
        

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserList>>> GetAll()
        {
            var userlist = await db.GetAll();
            if (userlist == null)
            {
                return NotFound();
            }
            return Ok(userlist);
        }
        [HttpGet(template: "{id}")]
        public async Task<ActionResult<UserList>> Get(string id)
        {
            var userlist = await db.Get(id);
            if (userlist == null)
            {
                return NotFound();
            }
            return Ok(userlist);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserList userList)
        {
            
            if (userList.UserName == null || userList.Name == null || userList.Surname == null || userList.Password == null)
            {
                return BadRequest("User name or/and name or/and surname or/and password souldn't be empty");
            }
            else
            {
                await db.Create(userList);
                return Created("Created", "Created");
            }
        }
        [HttpPut(template: "{id}")]
        public async Task<IActionResult> Put([FromBody] UserList userList, string id)
        {
            var usr = await db.Get(id);
            if (usr == null)
            {
                return NotFound("User with this id not found");
            }
            if (userList.UserName == null || userList.Name == null || userList.Surname == null || userList.Password == null)
            {
                return BadRequest("User name or/and name or/and surname or/and password souldn't be empty");
            }
            else
            {
                userList.Id = new string(id);
                await db.Put(userList);
                return Ok();
            }
        }
        [HttpDelete(template: "{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var userlist = await db.Get(id);
            if (userlist == null)
            {
                return NotFound();
            }
            else
            {
                await db.Delete(id);
                return NoContent();
            }
        }
    }
}
