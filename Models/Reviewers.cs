namespace PokemonReviewApp.Models
{
	public class Reviewers
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		ICollection<Reviews> Reviews { get; set;}
	}
}
