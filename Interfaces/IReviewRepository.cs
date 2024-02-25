using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
	public interface IReviewRepository
	{
		ICollection<Reviews> GetReviews();
		Reviews GetReview(int id);
		ICollection<Reviews> GetReviewsOfAPokemon(int pokemonId);
		bool ReviewExists(int reviewId);

	}
}
