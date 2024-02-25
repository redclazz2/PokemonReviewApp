using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
	public interface IReviewerRepository
	{
		ICollection<Reviewer> GetReviewers();
		Reviewer GetReviewer(int id);
		ICollection<Review> GetReviewsByReviewer(int id);
		bool ReviewerExists(int id);
	}
}
