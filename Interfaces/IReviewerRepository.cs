using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
	public interface IReviewerRepository
	{
		ICollection<Reviewers> GetReviewers();
		Reviewers GetReviewer(int id);
		ICollection<Reviews> GetReviewsByReviewer(int id);
		bool ReviewerExists(int id);
	}
}
