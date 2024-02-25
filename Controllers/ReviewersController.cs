using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;

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
	}
}
