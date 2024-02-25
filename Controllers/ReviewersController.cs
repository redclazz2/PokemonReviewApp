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
	public class ReviewersController : Controller
	{
		private IReviewerRepository _reviewerRepository;
		private IMapper _mapper;

		public ReviewersController(IReviewerRepository reviewerRepository, IMapper mapper)
		{
			this._reviewerRepository = reviewerRepository;
			this._mapper = mapper;
		}

		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<ReviewerDto>))]
		public IActionResult GetReviewers()
		{
			var reviews = _mapper.Map<IEnumerable<ReviewerDto>>(_reviewerRepository.GetReviewers());
			if (ModelState.IsValid)
			{
				return Ok(reviews);
			}
			else
			{
				return BadRequest(ModelState);
			}
		}


		[HttpGet("{id}")]
		[ProducesResponseType(200, Type = typeof(ReviewerDto))]
		[ProducesResponseType(400)]
		public IActionResult GetReviewer(int id)
		{
			if (!_reviewerRepository.ReviewerExists(id))
				return NotFound();

			var review = _mapper.Map<ReviewerDto>(_reviewerRepository.GetReviewer(id));
			if (ModelState.IsValid)
			{
				return Ok(review);
			}
			else
			{
				return BadRequest(ModelState);
			}
		}

		[HttpGet("ReviewsFromReviewer/{id}")]
		[ProducesResponseType(200, Type = typeof(List<ReviewDto>))]
		[ProducesResponseType(400)]
		public IActionResult GetReviewsFromAPokemon(int id)
		{
			var reviews = _mapper.Map<List<ReviewDto>>(_reviewerRepository.GetReviewsByReviewer(id));
			if (ModelState.IsValid)
			{
				return Ok(reviews);
			}
			else
			{
				return BadRequest(ModelState);
			}
		}

		[HttpPost]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		public IActionResult CreateReviewer([FromBody] ReviewerDto reviewerCreate)
		{
			if(reviewerCreate == null)
				return BadRequest(ModelState);

			var reviewer = _reviewerRepository.GetReviewers()
				.Where(r => r.FirstName == reviewerCreate.FirstName && r.LastName == reviewerCreate.LastName)
				.FirstOrDefault();

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var reviewerMap = _mapper.Map<Reviewer>(reviewerCreate);

			if (!_reviewerRepository.CreateReviewer(reviewerMap))
			{
				ModelState.AddModelError("", "Something went wrong when creating a reviewer");
				return StatusCode(500, ModelState);
			}

			return Ok("Success.");
		}
	}
}
