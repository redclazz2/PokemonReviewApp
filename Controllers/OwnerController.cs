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
	public class OwnerController : Controller
	{
		private IOwnerRepository _ownerRepository;
		private ICountryRepository _countryRepository;
		private IMapper _mapper;

		/*
			Owner controller needs to brind a country repository due the DB design and how the owner insertion works
		 */
		public OwnerController(IOwnerRepository ownerRepository, IMapper mapper, ICountryRepository countryRepository)
		{
			this._ownerRepository = ownerRepository;
			this._mapper = mapper;
			this._countryRepository = countryRepository;
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
		[ProducesResponseType(200, Type = typeof(OwnerDto))]
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
		[ProducesResponseType(200, Type = typeof(List<Pokemon>))]
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

		[HttpPost]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		public IActionResult CreateCountry([FromQuery] int CountryId,[FromBody] OwnerDto ownerCreate)
		{
			/*
			 This method let's you send a country id via URL on a post request.
			 */

			if (ownerCreate == null)
				return BadRequest(ModelState);

			var owners = _ownerRepository.GetOwners().
				Where(c => c.LastName.Trim().ToUpper() == ownerCreate.LastName.TrimEnd().ToUpper()).FirstOrDefault();

			if (owners != null)
			{
				ModelState.AddModelError("", "Country Already Exists!");
				return StatusCode(422, ModelState);
			}

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var ownerMap = _mapper.Map<Owner>(ownerCreate);

			ownerMap.Country = _countryRepository.GetCountry(CountryId);

			if (!_ownerRepository.CreateOwner(ownerMap))
			{
				ModelState.AddModelError("", "Something went wrong when creating a country");
				return StatusCode(500, ModelState);
			}

			return Ok("Success");
		}
	}
}
