namespace PokemonReviewApp.Models
{
	public class Owners
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gym { get; set; }

		public Countries Country { get; set; }
		public ICollection<PokemonOwners> PokemonOwners { get; set; }
	}
}
