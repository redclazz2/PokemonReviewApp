namespace PokemonReviewApp.Models
{
	public class Countries
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public ICollection<Owners> Owners { get; set; }
	}
}
