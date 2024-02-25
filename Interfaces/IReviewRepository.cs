using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
	public interface IReviewRepository
	{
		ICollection<Review> GetReviews();
		Review GetReview(int id);
		ICollection<Review> GetReviewsOfAPokemon(int pokemonId);
		bool ReviewExists(int reviewId);

	}
}
