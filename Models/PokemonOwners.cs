namespace PokemonReviewApp.Models
{
	public class PokemonOwners
	{
		public int PokemonId { get; set; }
		public int OwnerId {  get; set; }
		public Pokemons Pokemon { get; set; }
		public Owners Owner { get; set; }
	}
}
