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

	public class PokemonController:Controller
	{
        private readonly IMapper _mapper;
		public IPokemonRepository _pokemonRepository { get; set; }

        public PokemonController(IPokemonRepository pokemonRepository, IMapper mapper)
        {
            _pokemonRepository = pokemonRepository;
			this._mapper = mapper;
		}

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        public IActionResult getPokemons()
        {
            var pokemons = _mapper.Map<List<PokemonDto>>(_pokemonRepository.GetPokemons());
            if(ModelState.IsValid)
            {
                return Ok(pokemons);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

		[HttpGet("{Id}")]
		[ProducesResponseType(200, Type = typeof(Pokemon))]
		[ProducesResponseType(400)]
        public IActionResult getPokemon(int Id)
        {
            if (!_pokemonRepository.PokemonExists(Id))
                return NotFound();

            var pokemon = _mapper.Map<PokemonDto>(_pokemonRepository.GetPokemon(Id));
			if (ModelState.IsValid)
			{
				return Ok(pokemon);
			}
			else
			{
				return BadRequest(ModelState);
			}

		}

        [HttpGet("Rating/{Id}")]
		[ProducesResponseType(200, Type = typeof(decimal))]
		[ProducesResponseType(400)]
        public IActionResult getPokemonRating(int Id)
        {
            if (!_pokemonRepository.PokemonExists(Id))
                return NotFound();
            
            var rating = _pokemonRepository.GetPokemonRating(Id);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(rating);
        }

        [HttpPost]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
        public IActionResult CreatePokemon(
            [FromQuery] int ownerId, 
            [FromQuery] int categoryId, 
            [FromBody] PokemonDto pokemonCreate)
        {
            if (pokemonCreate == null)
                return BadRequest(ModelState);

            var pokemon = _pokemonRepository.GetPokemons()
                .Where(p => p.Name.Trim().ToUpper() ==  pokemonCreate.Name.Trim().ToUpper()).ToList();

            if(pokemon == null)
            {
				ModelState.AddModelError("", "Pokemon Already Exists!");
				return StatusCode(422, ModelState);
			}

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var pokemonMap = _mapper.Map<Pokemon>(pokemonCreate);

            if(!_pokemonRepository.CreatePokemon(categoryId, ownerId, pokemonMap))
            {
				ModelState.AddModelError("", "Something went wrong when creating a pokemon");
				return StatusCode(500, ModelState);
			}

            return Ok("Success.");
        }
	}
}
