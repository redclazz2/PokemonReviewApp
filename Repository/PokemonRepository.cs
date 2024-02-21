using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
	public class PokemonRepository : IPokemonRepository
	{
		private readonly DataContext _context;
		public PokemonRepository(DataContext context) 
		{
			_context = context;
		}

		public Pokemons GetPokemon(int id)
		{
			return _context.Pokemon.Where(p => p.Id == id).FirstOrDefault();
		}

		public Pokemons GetPokemon(string name)
		{
			return _context.Pokemon.Where(p => p.Name == name).FirstOrDefault();
		}

		public decimal GetPokemonRating(int id)
		{
			var review = _context.Reviews.Where(p => p.Id == id);
			if(review.Count() <= 0)
			{
				return 0;
			}
			return (decimal)(review.Sum(r => r.Rating) / review.Count());
		}

		public ICollection<Pokemons> GetPokemons()
		{
			return _context.Pokemon.OrderBy(p => p.Id).ToList();
		}

		public bool PokemonExists(int id)
		{
			return _context.Pokemon.Any(p => p.Id == id);
		}
	}
}
