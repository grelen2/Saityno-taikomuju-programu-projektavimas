using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestGreta.Data.Entities;
using RestGreta.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace RestGreta.Controllers
{
    
    [Route("api/comments")]
    [ApiController]
    public class CommentController: ControllerBase
    {
       
        ICommentRepository db = new CommentRepository();
       
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetAll()
        {
            var comms = await db.GetAll();
            if(comms == null)
            {
                return NotFound();
            }
            return Ok(comms);
        }

        
        [HttpGet(template: "{id}")]
        public async Task<ActionResult<Comment>> Get(string id)
        {
            var comms = await db.Get(id);
            if (comms == null)
            {
                return NotFound("Comment with this id not found");
            }
            return Ok(comms);

        }

        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Comment comment)
        {

            if (comment.CommentText == null || comment.UserName == null)
            {
                //ModelState.AddModelError("CommentText", "The comment text shouldn't be empty");
                return BadRequest("The comment text or/and user name shouldn't be empty");
            }
            else
            {
                await db.Create(comment);
                return Created("Created", "Created");
            }
        }

        
        [HttpPut(template: "{id}")]
        public async Task<IActionResult> Put([FromBody] Comment comment, string id)
        {
            var com = await db.Get(id);
            if (com == null)
            {
                return NotFound();
            }
            if (comment.CommentText == null || comment.UserName == null)
            {
                return BadRequest("The comment text or/and user name shouldn't be empty");
            }
            else
            {
                comment.Id = new string(id);
                await db.Put(comment);
                return Ok();
            }
        }
        

        [HttpDelete(template:"{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var comment = await db.Get(id);
            if (comment == null)
            {
                return NotFound();
            }
            else
            {
                await db.Delete(id);
                return Ok();
            }
        }
    }
}
