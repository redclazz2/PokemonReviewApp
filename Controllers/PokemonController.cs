using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

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
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemons>))]
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
		[ProducesResponseType(200, Type = typeof(Pokemons))]
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

        [HttpGet("{Id}/rating")]
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
	}
}
