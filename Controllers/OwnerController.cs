using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OwnerController : Controller
	{
		private IOwnerRepository _ownerRepository;
		private IMapper _mapper;

		public OwnerController(IOwnerRepository ownerRepository, IMapper mapper)
		{
			this._ownerRepository = ownerRepository;
			this._mapper = mapper;
		}

		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<OwnerDto>))]
		public IActionResult GetOwners() {
			var owners = _mapper.Map<IEnumerable<OwnerDto>>(_ownerRepository.GetOwners());
			if (ModelState.IsValid)
			{
				return Ok(owners);
			}
			else
			{
				return BadRequest(ModelState);
			}
		}

		[HttpGet("{id}")]
		[ProducesResponseType(200, Type = typeof(Owners))]
		[ProducesResponseType(400)]
		public IActionResult GetOwner(int id) {
			if (!_ownerRepository.OwnerExists(id))
				return NotFound();

			var owner = _mapper.Map<OwnerDto>(_ownerRepository.GetOwner(id));
			if (ModelState.IsValid)
			{
				return Ok(owner);
			}
			else
			{
				return BadRequest(ModelState);
			}
		}

		[HttpGet("OwnersFromPokemon/{pokemonId}")]
		[ProducesResponseType(200, Type = typeof(List<OwnerDto>))]
		[ProducesResponseType(400)]
		public IActionResult GetOwnersOfAPokemon(int pokemonId) {
			var owners = _mapper.Map<List<OwnerDto>>(_ownerRepository.GetOwnersOfAPokemon(pokemonId));
			if (ModelState.IsValid)
			{
				return Ok(owners);
			}
			else
			{
				return BadRequest(ModelState);
			}
		}

		[HttpGet("PokemonFromOwner/{ownerId}")]
		[ProducesResponseType(200, Type = typeof(List<Pokemons>))]
		[ProducesResponseType(400)]
		public IActionResult GetPokemonByOwner(int ownerId)
		{
			var pokemons = _mapper.Map<IEnumerable<PokemonDto>>(_ownerRepository.GetPokemonByOwner(ownerId));
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
