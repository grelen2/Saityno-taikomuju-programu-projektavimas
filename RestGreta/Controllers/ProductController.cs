using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestGreta.Data.Entities;
using RestGreta.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestGreta.Controllers
{
    [Route("api/products")]
    [ApiController]
    

    /*product
    /api/product GET ALL 200
    /api/product/{id} GET 200
    /api/product POST 201
    /api/product/{id} PUT 200
    /api/product/{id} DELETE 204*/
    public class ProductsController : ControllerBase
    {
        IProductsRepository db = new ProductsRepository();

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            var products = await db.GetAll();
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }

        [HttpGet(template:"{id}")]
        public async Task<ActionResult<Product>> Get(string id)
        {
            var products = await db.Get(id);
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);

        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Product product)
        {

            if (product.Name == null)
            {
                ModelState.AddModelError("name", "The product name shouldn't be empty");
                return BadRequest("The product name shouldn't be empty");
            }
            else {
                await db.Create(product);
                return Created("Created", "Created");
            }
        }

        [HttpPut(template: "{id}")]
        public async Task<IActionResult> Put([FromBody] Product product, string id)
        {
            var prod = await db.Get(id);
            if (prod == null)
            {
              //  if (product == null)
            //{
                return NotFound("Product with this id not found");
            }
            else if( product.Name == null)
            {
                ModelState.AddModelError("name", "The product name shouldn't be empty");
                return BadRequest("The product name shouldn't be empty");
            }
            else{
                product.Id = new string(id);
                await db.Put(product);
                return Ok();
            }
        }
        [HttpDelete(template: "{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var product = await db.Get(id);
            if (product == null)
            {
                return NotFound("Product with this id not found");
            }
            else
            {
                await db.Delete(id);
                return NoContent();
            }
        }

    }
}
