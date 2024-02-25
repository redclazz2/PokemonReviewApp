using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
	public class CategoryRepository : ICategoryRepository
	{
		private DataContext _context;

        public CategoryRepository(DataContext context)
        {
			_context = context;       
        }

        public bool CategoryExists(int id)
		{
			return _context.Categories.Any(c => c.Id == id);
		}

		public ICollection<Categories> GetCategories()
		{
			return _context.Categories.OrderBy(c => c.Id).ToList();
		}

		public Categories GetCategory(int id)
		{
			return _context.Categories.Where(c => c.Id == id).FirstOrDefault();
		}

		public ICollection<Pokemons> GetPokemonByCategory(int id)
		{
			//You gotta select the pokemon from the nested (Entity Framework) Entity.
			return _context.PokemonCategories.Where(e => e.CategoryId == id).Select(c => c.Pokemon).ToList();
		}
	}
}
