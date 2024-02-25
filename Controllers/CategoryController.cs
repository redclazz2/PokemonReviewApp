using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repository;

namespace PokemonReviewApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController:Controller
	{
        private ICategoryRepository _categoryRepository;
        private IMapper _mapper;
        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            this._categoryRepository = categoryRepository;
            this._mapper = mapper;
        }

		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Categories>))]
		public IActionResult getCategories()
		{
			var categories = _mapper.Map<List<CategoryDto>>(_categoryRepository.GetCategories());
			if (ModelState.IsValid)
			{
				return Ok(categories);
			}
			else
			{
				return BadRequest(ModelState);
			}
		}

		[HttpGet("{id}")]
		[ProducesResponseType(200, Type = typeof(Categories))]
		[ProducesResponseType(400)]
		public IActionResult getCategory(int id)
		{
			if(!_categoryRepository.CategoryExists(id))
				return NotFound();

			var category = _mapper.Map<CategoryDto>(_categoryRepository.GetCategory(id));
			if (ModelState.IsValid)
			{
				return Ok(category);
			}
			else
			{
				return BadRequest(ModelState);
			}
		}

		[HttpGet("Pokemon/{id}")]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Pokemons>))]
		[ProducesResponseType(400)]
		public IActionResult getPokemonByCategory(int id)
		{
			if (!_categoryRepository.CategoryExists(id))
				return NotFound();

			var pokemons = _mapper.Map<List<PokemonDto>>(_categoryRepository.GetPokemonByCategory(id));
			
			if (ModelState.IsValid)
			{
				return Ok(pokemons);
			}
			else
			{
				return BadRequest(ModelState);
			}
		}
	}
}
