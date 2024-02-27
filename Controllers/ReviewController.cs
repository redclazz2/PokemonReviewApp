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


		[HttpPost]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		public IActionResult CreateReview([FromQuery] int pokemonId, [FromQuery] int reviewerId,[FromBody] ReviewDto reviewCreate)
		{
			if (reviewCreate == null)
				return BadRequest(ModelState);

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var reviewMap = _mapper.Map<Review>(reviewCreate);

			if (!_reviewRepository.CreateReview(pokemonId,reviewerId,reviewMap))
			{
				ModelState.AddModelError("", "Something went wrong when creating a review");
				return StatusCode(500, ModelState);
			}

			return Ok("Success.");
		}


		[HttpPut("{reviewId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public IActionResult UpdateReviewer(int reviewId, [FromBody] ReviewDto updatedReview)
		{
			if (updatedReview == null)
				return BadRequest(ModelState);

			if (reviewId != updatedReview.Id)
				return BadRequest(ModelState);

			if (!_reviewRepository.ReviewExists(reviewId))
				return NotFound();

			if (!ModelState.IsValid)
				return BadRequest();

			var reviewMap = _mapper.Map<Review>(updatedReview);
			if (!_reviewRepository.UpdateReview(reviewMap))
			{
				ModelState.AddModelError("", "Something went wrong when updating the review");
				return StatusCode(500, ModelState);
			}

			return NoContent();
		}
	}
}
