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
		[ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
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
		[ProducesResponseType(200, Type = typeof(Category))]
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
		[ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
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

		[HttpPost]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		public IActionResult CreateCategory([FromBody] CategoryDto categoryCreate)
		{
			if(categoryCreate == null)
				return BadRequest(ModelState);
			
			var category = _categoryRepository.GetCategories().
				Where(c=> c.Name.Trim().ToUpper() == categoryCreate.Name.TrimEnd().ToUpper()).FirstOrDefault();

			if(category != null)
			{
				ModelState.AddModelError("", "Category Already Exists!");
				return StatusCode(422, ModelState);
			}

			if(!ModelState.IsValid)
				return BadRequest(ModelState);

			var categoryMap = _mapper.Map<Category>(categoryCreate);

			if (!_categoryRepository.CreateCategory(categoryMap)) {
				ModelState.AddModelError("", "Something went wrong when creating a category");
				return StatusCode(500,ModelState);
			}

			return Ok("Success");
		}

		[HttpPut("{categoryId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public IActionResult UpdateCategory(int categoryId, [FromBody] CategoryDto updatedCategory) {
			if (updatedCategory == null)
				return BadRequest(ModelState);

			if(categoryId != updatedCategory.Id)
				return BadRequest(ModelState);
			
			if(!_categoryRepository.CategoryExists(categoryId))
				return NotFound();

			if(!ModelState.IsValid)
				return BadRequest();

			var categoryMap = _mapper.Map<Category>(updatedCategory);
			if(!_categoryRepository.UpdateCategory(categoryMap)) {
				ModelState.AddModelError("", "Something went wrong when updating the category");
				return StatusCode(500,ModelState);
			}

			return NoContent();
		}
	}
}
