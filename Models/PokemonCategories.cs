namespace PokemonReviewApp.Models
{
	public class PokemonCategories
	{
		public int PokemonId { get; set; }
		public int CategoryId { get; set; }
		public Pokemons Pokemon { get; set; }
		public Categories Category { get; set; }
	}
}
