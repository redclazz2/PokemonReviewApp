using AutoMapper;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
	public class ReviewRepository : IReviewRepository
	{
		public DataContext _context { get; }
		public IMapper _mapper { get; }

		public ReviewRepository(DataContext context, IMapper mapper)
        {
			_context = context;
			_mapper = mapper;
		}

		public Review GetReview(int id)
		{
			return _context.Reviews.Where(r => r.Id == id).FirstOrDefault();	
		}

		public ICollection<Review> GetReviews()
		{
			return _context.Reviews.OrderBy(r => r.Id).ToList();
		}

		public ICollection<Review> GetReviewsOfAPokemon(int pokemonId)
		{
			return _context.Reviews.Where(r => r.Pokemon.Id == pokemonId).ToList();
		}

		public bool ReviewExists(int reviewId)
		{
			return _context.Reviews.Any(r => r.Id == reviewId);
		}

		public bool Save()
		{
			var _saved = _context.SaveChanges();
			return _saved > 0 ? true : false;
		}

		public bool CreateReview(int pokemonId, int reviewerId, Review review)
		{
			var pokemon = _context.Pokemon.Where(p => p.Id == pokemonId).FirstOrDefault();
			var reviewer = _context.Reviewers.Where(r => r.Id == reviewerId).FirstOrDefault();

			review.Pokemon = pokemon;
			review.Reviewer = reviewer;

			_context.Add(review);
			return Save();
		}
	}
}
