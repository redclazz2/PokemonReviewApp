namespace PokemonReviewApp.Models
{
	public class Pokemon
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
	}
}

//Models are sometimes also called "pocos" and in ASP.NET they contain the properties from a SQL table.