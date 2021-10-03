using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestGreta.Data.Dtos.Recipies;
using RestGreta.Data.Entities;
using RestGreta.Data.Repositories;

namespace RestGreta.Controllers
{
    [ApiController]
    [Route("api/recipies")]
    public class RecipeController :ControllerBase
    {
        private readonly IRecipesRepository _recipiesRepository;
        private readonly IMapper _mapper;
        public RecipeController(IRecipesRepository recipiesRepository, IMapper mapper)
        {
            _recipiesRepository = recipiesRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<RecipeDto>> GetAll()
        {
            return (await _recipiesRepository.GetAll()).Select(o => _mapper.Map<RecipeDto>(o));
        }
        [HttpGet(template: "{id}")]
        public async Task<ActionResult<RecipeDto>> Get(int id)
        {
            var recipiesList = await _recipiesRepository.Get(id);
            if (recipiesList == null) return NotFound($"Recipe with id'{id}'not found");
            return _mapper.Map<RecipeDto>(recipiesList);


        }
        [HttpPost]
        public async Task<ActionResult<RecipeDto>> Post(CreateRecipeDto recipiesDto)
        {
            var recipiesList = _mapper.Map<Recipe>(recipiesDto);
            await _recipiesRepository.Create(recipiesList);
            return Created($"/api/recipies/{recipiesList.Id}", _mapper.Map<RecipeDto>(recipiesList));
        }
        [HttpPut(template: "{id}")]
        public async Task<ActionResult<RecipeDto>> Put(int id, UpdateRecipeDto recipiesDto)
        {
            var recipiesList = await _recipiesRepository.Get(id);
            if (recipiesList == null) return NotFound($"Recipe with id'{id}'not found");

            recipiesList.RecipeName = recipiesDto.RecipeName;
            recipiesList.ProductName = recipiesDto.ProductName;
            recipiesList.Quantity = recipiesDto.Quantity;
            recipiesList.SeesUnits = recipiesDto.SeesUnits;
            recipiesList.Description = recipiesDto.Description;



            await _recipiesRepository.Put(recipiesList);
            return _mapper.Map<RecipeDto>(recipiesList);
        }
        [HttpDelete(template: "{id}")]
        public async Task<ActionResult<RecipeDto>> Delete(int id)
        {
            var recipiesList = await _recipiesRepository.Get(id);
            if (recipiesList == null) return NotFound($"Recipe with id'{id}'not found");

            await _recipiesRepository.Delete(recipiesList);
            return NoContent();
        }
    }
}
