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
	public class ReviewController : Controller
	{
		private IReviewRepository _reviewRepository;
		private IMapper _mapper;

        public ReviewController(IReviewRepository reviewRepository, IMapper mapper)
        {
            this._reviewRepository = reviewRepository;
			this._mapper = mapper;
        }

		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDto>))]
		public IActionResult GetReviews()
		{
			var reviews = _mapper.Map<IEnumerable<ReviewDto>>(_reviewRepository.GetReviews());
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
		[ProducesResponseType(200, Type = typeof(ReviewDto))]
		[ProducesResponseType(400)]
		public IActionResult GetReview(int id)
		{
			if (!_reviewRepository.ReviewExists(id))
				return NotFound();

			var review = _mapper.Map<ReviewDto>(_reviewRepository.GetReview(id));
			if (ModelState.IsValid)
			{
				return Ok(review);
			}
			else
			{
				return BadRequest(ModelState);
			}
		}

		[HttpGet("ReviewsFromPokemon/{id}")]
		[ProducesResponseType(200, Type = typeof(ReviewDto))]
		[ProducesResponseType(400)]
		public IActionResult GetReviewsFromAPokemon(int id)
		{
			var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviewsOfAPokemon(id));
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
