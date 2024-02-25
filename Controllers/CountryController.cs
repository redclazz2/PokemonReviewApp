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
	public class CountryController:Controller
	{
        public ICountryRepository _countryRepository { get; set; }
		public IMapper _mapper { get; set; }

		public CountryController(ICountryRepository countryRepository, IMapper mapper)
		{
			this._mapper = mapper;
			this._countryRepository = countryRepository;
		}

		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<CountryDto>))]
		public IActionResult getCountries()
		{
			var countries = _mapper.Map<List<CountryDto>>(_countryRepository.GetCountries());
			if (ModelState.IsValid)
			{
				return Ok(countries);
			}
			else
			{
				return BadRequest(ModelState);
			}
		}

		[HttpGet("{id}")]
		[ProducesResponseType(200, Type = typeof(IEnumerable<CountryDto>))]
		[ProducesResponseType(400)]
		public IActionResult getCountry(int id)
		{
			if (!_countryRepository.CountryExists(id))
				return NotFound();

			var country = _mapper.Map<CountryDto>(_countryRepository.GetCountry(id));
			if(ModelState.IsValid)
			{
				return Ok(country);
			}
			else
			{
				return BadRequest(ModelState);
			}
		}

		[HttpGet("CountriesByOwner/{id}")]
		[ProducesResponseType(200, Type = typeof(IEnumerable<CountryDto>))]
		[ProducesResponseType(400)]
		public IActionResult getCountryByOwner(int id)
		{
			var country = _mapper.Map<CountryDto>(_countryRepository.GetCountryByOwner(id));
			if (ModelState.IsValid)
			{
				return Ok(country);
			}
			else
			{
				return BadRequest(ModelState);
			}
		}

		[HttpGet("OwnersByCountry/{id}")]
		[ProducesResponseType(200, Type = typeof(IEnumerable<OwnerDto>))]
		[ProducesResponseType(400)]
		public IActionResult getOwnersByCountry(int id)
		{
			var country = _mapper.Map<List<OwnerDto>>(_countryRepository.GetOwnersFromACountry(id));
			if (ModelState.IsValid)
			{
				return Ok(country);
			}
			else
			{
				return BadRequest(ModelState);
			}
		}
	}
}
