namespace PokemonReviewApp.Models
{
	public class Reviews
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Text { get; set; }
		public int Rating {  get; set; }
		public Reviewers Reviewer{ get; set;}
        public Pokemons Pokemon{ get; set; }
    }
}
