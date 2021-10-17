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
        public async Task<ActionResult<IEnumerable<Comment>>> GetAllComments()
        {
            var comms = await db.GetAllComments();
            if(comms == null)
            {
                return NotFound();
            }
            return Ok(comms);
        }

        
        [HttpGet(template: "{id}")]
        public async Task<ActionResult<Comment>> GetComment(string id)
        {
            var comms = await db.GetComment(id);
            if (comms == null)
            {
                return NotFound("Comment with this id not found");
            }
            return Ok(comms);

        }

        
        [HttpPost]
        public async Task<IActionResult> PostComment([FromBody] Comment comment)
        {

            if (comment.CommentText == null || comment.UserName == null)
            {
                //ModelState.AddModelError("CommentText", "The comment text shouldn't be empty");
                return BadRequest("The comment text or/and user name shouldn't be empty");
            }
            else
            {
                await db.CreateComment(comment);
                return Created("Created", "Created");
            }
        }

        
        [HttpPut(template: "{id}")]
        public async Task<IActionResult> PutComment([FromBody] Comment comment, string id)
        {
            var com = await db.GetComment(id);
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
                await db.PutComment(comment);
                return Ok();
            }
        }
        

        [HttpDelete(template:"{id}")]
        public async Task<IActionResult> DeleteComment(string id)
        {
            var comment = await db.GetComment(id);
            if (comment == null)
            {
                return NotFound();
            }
            else
            {
                await db.DeleteComment(id);
                return NoContent();
            }
        }
    }
}
