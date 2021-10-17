using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestGreta.Data.Entities;
using RestGreta.Data.Repositories;
using MongoDB.Bson;

namespace RestGreta.Controllers
{

    [Route("api/recipies")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        IRecipesRepository db = new RecipesRepository();


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetAll()
        {
            var recipes = await db.GetAll();
            if (recipes == null)
            {
                return NotFound();
            }
            return Ok(recipes);
        }
        [HttpGet(template: "{id}")]
        public async Task<ActionResult<Recipe>> Get(string id)
        {
            var recipe = await db.Get(id);
            if (recipe == null)
            {
                return NotFound();
            }
            return Ok(recipe);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Recipe recipe)
        {
            if (recipe.RecipeName == null || recipe.ProductName == null || recipe.Quantity == null || recipe.SeesUnits == null || recipe.Description == null)
            {
                return BadRequest("RecipeName or/and ProductName or/and Quantity or/and SeesUnits or/and Description shouldn't be empty");
            }
            else
            {
                await db.Create(recipe);
                return Created("Created", "Created");
            }
        }
        [HttpPut(template: "{id}")]
        public async Task<IActionResult> Put([FromBody] Recipe recipe, string id)
        {
            var rec = await db.Get(id);
            if (rec == null)
            {
                return NotFound("Recipe with this id not found");
            }
            if (recipe.RecipeName == null || recipe.ProductName == null || recipe.Quantity == null || recipe.SeesUnits == null || recipe.Description == null)
            {
                return BadRequest("RecipeName or/and ProductName or/and Quantity or/and SeesUnits or/and Description shouldn't be empty");
            }
            else
            {
                recipe.Id = new string(id);
                await db.Put(recipe);
                return Ok();
            }
        }
        [HttpDelete(template: "{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var recipe = await db.Get(id);
            if (recipe == null)
            {
                return NotFound("Recipe with this id not found");
            }
            else
            {
                await db.Delete(id);
                return NoContent();
            }
        }
        //-------------------------------------------------------------------------
        [HttpGet("{id}/comment")]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetAllComments(string id)
        {
            var recipe = await db.Get(id);
            if (recipe == null)
            {
                return NotFound("Recipe with this id not found");
            }
            var x = await db.GetAllComments(id);
            if (x == null)
            {
                return NotFound("This recipie do not have comments");
            }
            return Ok(x);
        }
        [HttpGet("{id}/comment/{commentId}")]
        public async Task<ActionResult<Comment>> GetComment(string id, string commentId)
        {
            var recipe = await db.Get(id);
            if (recipe == null)
            {
                return NotFound("Recipe with this id not found");
            }
            var comment = await db.GetComment(id, commentId);
            if (comment == null)
            {
                return NotFound("Comment with this id not found");
            }
            return Ok(comment);
        }
        [HttpPost("{id}/comment")]
        public async Task<IActionResult> CreateComment([FromBody] Comment comment, string id)
        {
            var recipe = await db.Get(id);
            if (recipe == null)
            {
                return NotFound("Recipe with this id not found");
            }
            if (comment.CommentText == null || comment.UserName == null)
            {
                return BadRequest("The comment text or/and user name shouldn't be empty");
            }
            await db.CreateComment(comment, id);
            return Created("Created", true);
        }
        [HttpPut("{id}/comment/{commentId}")]
        public async Task<IActionResult> PutComment([FromBody] Comment comment, string id, string commentId)
        {
            var recipe = await db.Get(id);
            if (recipe == null)
            {
                return NotFound("Recipe with this id not found");
            }
            if (comment == null)
            {
                return NotFound("Comment with this id not found");
            }
            if (comment.CommentText == null || comment.UserName == null)
            {
                return BadRequest("The comment text or/and user name shouldn't be empty");
            }
            comment.Id = new string(commentId);
            await db.PutComment(comment, id, commentId);
            return Ok("Comment updated");
        }
        [HttpDelete("{id}/comment/{commentId}")]
        public async Task<IActionResult> DeleteComment(string id, string commentId)
        {
            var recipe = await db.Get(id);
            var comm = await db.GetComment(id, commentId);
            if (recipe == null)
            {
                return NotFound("Recipe with this id not found");
            }
            if (comm == null)
            {
                return NotFound("Comment with this id not found");
            }
            await db.DeleteComment(id, commentId);
            return NoContent();
        }

    }
}
