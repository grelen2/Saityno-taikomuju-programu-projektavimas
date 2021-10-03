using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestGreta.Data.Dtos.Products;
using RestGreta.Data.Entities;
using RestGreta.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestGreta.Controllers
{
    [ApiController]
    [Route("api/products")]

    /*product
    /api/product GET ALL 200
    /api/product/{id} GET 200
    /api/product POST 201
    /api/product/{id} PUT 200
    /api/product/{id} DELETE 204*/
    public class ProductsController : ControllerBase
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IMapper _mapper;
        public ProductsController(IProductsRepository productsRepository, IMapper mapper)
        {
            _productsRepository = productsRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductDto>> GetAll()
        {
            return (await _productsRepository.GetAll()).Select(o => _mapper.Map<ProductDto>(o));
        }
        [HttpGet(template:"{id}")]
        public async Task<ActionResult<ProductDto>> Get(int id)
        {
            var product = await _productsRepository.Get(id);
            if (product == null) return NotFound($"Product with id'{id}'not found");
            return _mapper.Map<ProductDto>(product);


        }
        [HttpPost]
        public async Task<ActionResult<ProductDto>> Post(CreateProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            await _productsRepository.Create(product);
            return Created($"/api/products/{product.Id}",_mapper.Map<ProductDto>(product));
        }
        [HttpPut(template: "{id}")]
        public async Task<ActionResult<ProductDto>> Put(int id, UpdateProductDto productDto)
        {
            var product = await _productsRepository.Get(id);
            if (product == null) return NotFound($"Product with id'{id}'not found");

            product.Name = productDto.Name;

            await _productsRepository.Put(product);
            return _mapper.Map<ProductDto>(product);
        }
        [HttpDelete(template: "{id}")]
        public async Task<ActionResult<ProductDto>> Delete(int id)
        {
            var product = await _productsRepository.Get(id);
            if (product == null) return NotFound($"Product with id'{id}'not found");

            await _productsRepository.Delete(product);
            return NoContent();
        }

    }
}
