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
    public class RecipeController :ControllerBase
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
                return Ok();
            }
        }
    }
}
