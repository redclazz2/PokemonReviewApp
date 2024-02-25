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

		public bool CreateCategory(Category category)
		{
			//Change Tracker
			//Is this adding, updating or modifying?
			//Conected vs Disconnected
			_context.Add(category);
			return Save();
		}

		public ICollection<Category> GetCategories()
		{
			return _context.Categories.OrderBy(c => c.Id).ToList();
		}

		public Category GetCategory(int id)
		{
			return _context.Categories.Where(c => c.Id == id).FirstOrDefault();
		}

		public ICollection<Pokemon> GetPokemonByCategory(int id)
		{
			//You gotta select the pokemon from the nested (Entity Framework) Entity.
			return _context.PokemonCategories.Where(e => e.CategoryId == id).Select(c => c.Pokemon).ToList();
		}

		public bool Save()
		{
			//Actual SQL will be created and sent to the database.
			var _saved = _context.SaveChanges();
			return _saved > 0 ? true : false;
		}
	}
}
