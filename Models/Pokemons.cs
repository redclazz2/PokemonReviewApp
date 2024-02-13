namespace PokemonReviewApp.Models
{
	public class Pokemons
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime BirthDate { get; set; }

		public ICollection<Reviews> Reviews { get; set; }
		public ICollection<PokemonOwners> PokemonOwners { get; set; }
		public ICollection<PokemonCategories> PokemonCategories { get; set; }
	}
}

//Models are sometimes also called "pocos" and in ASP.NET they contain the properties from a SQL table.