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

		public bool CreatePokemon(int categoryId, int ownerId, Pokemon pokemon)
		{
			/*
			 Pokemon is an entity that has multiple relations with:
			 Review, Owner and Category.
			 
			 In order to add a pokemon you need to specify it's
			 owner due the DB structure.
				
			 That's the reason why the many to many entity Pokemon owner is created here.
			 
			 Same reason as why the pokemonCategory needs to be created first too.
			 */
			var owner = _context.Owners.Where(x => x.Id == ownerId).FirstOrDefault();
			var category = _context.Categories.Where(c => c.Id == categoryId).FirstOrDefault();

			var pokemonOwner = new PokemonOwners()
			{
				Owner = owner,
				Pokemon = pokemon,
			};

			_context.Add(pokemonOwner);

			var pokemonCategory = new PokemonCategories()
			{
				Category = category,
				Pokemon = pokemon,
			};

			_context.Add(pokemonCategory);

			_context.Add(pokemon);
			return Save();
		}

		public Pokemon GetPokemon(int id)
		{
			return _context.Pokemon.Where(p => p.Id == id).FirstOrDefault();
		}

		public Pokemon GetPokemon(string name)
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

		public ICollection<Pokemon> GetPokemons()
		{
			return _context.Pokemon.OrderBy(p => p.Id).ToList();
		}

		public bool PokemonExists(int id)
		{
			return _context.Pokemon.Any(p => p.Id == id);
		}

		public bool Save()
		{
			var _saved = _context.SaveChanges();
			return _saved > 0 ? true : false;
		}

		public bool UpdatePokemon(int ownerId,int categoryId ,Pokemon pokemon) {
			_context.Update(pokemon);
			return Save();
		}
	}
}
