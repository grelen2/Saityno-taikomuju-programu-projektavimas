using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestGreta.Data.Entities;
using RestGreta.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

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

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            var products = await db.GetAllProducts();
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }

        [AllowAnonymous]
        [HttpGet(template:"{id}")]
        public async Task<ActionResult<Product>> GetProduct(string id)
        {
            var products = await db.GetProduct(id);
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);

        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> PostProduct([FromBody] Product product)
        {

            if (product.Name == null)
            {
                ModelState.AddModelError("name", "The product name shouldn't be empty");
                return BadRequest("The product name shouldn't be empty");
            }
            else {
                await db.CreateProduct(product);
                return Created("Created", "Created");
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPut(template: "{id}")]
        public async Task<IActionResult> PutProduct([FromBody] Product product, string id)
        {
            var prod = await db.GetProduct(id);
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
                await db.PutProduct(product);
                return Ok();
            }
        }
        [Authorize(Roles ="Admin")]
        [HttpDelete(template: "{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var product = await db.GetProduct(id);
            if (product == null)
            {
                return NotFound("Product with this id not found");
            }
            else
            {
                await db.DeleteProduct(id);
                return NoContent();
            }
        }

    }
}
