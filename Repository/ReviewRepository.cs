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
	}
}
