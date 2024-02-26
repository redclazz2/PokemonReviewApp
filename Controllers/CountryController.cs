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

		[HttpPost]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		public IActionResult CreateCountry([FromBody] CountryDto countryCreate)
		{
			if (countryCreate == null)
				return BadRequest(ModelState);

			var country = _countryRepository.GetCountries().
				Where(c => c.Name.Trim().ToUpper() == countryCreate.Name.TrimEnd().ToUpper()).FirstOrDefault();

			if (country != null)
			{
				ModelState.AddModelError("", "Country Already Exists!");
				return StatusCode(422, ModelState);
			}

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var countryMap = _mapper.Map<Country>(countryCreate);

			if (!_countryRepository.CreateCountry(countryMap))
			{
				ModelState.AddModelError("", "Something went wrong when creating a country");
				return StatusCode(500, ModelState);
			}

			return Ok("Success");
		}

		[HttpPut("{countryId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public IActionResult UpdateCategory(int countryId, [FromBody] CountryDto updatedCountry)
		{
			if (updatedCountry == null)
				return BadRequest(ModelState);

			if (countryId != updatedCountry.Id)
				return BadRequest(ModelState);

			if (!_countryRepository.CountryExists(countryId))
				return NotFound();

			if (!ModelState.IsValid)
				return BadRequest();

			var countryMap = _mapper.Map<Country>(updatedCountry);
			if (!_countryRepository.UpdateCountry(countryMap))
			{
				ModelState.AddModelError("", "Something went wrong when updating the country");
				return StatusCode(500, ModelState);
			}

			return NoContent();
		}
	}
}
