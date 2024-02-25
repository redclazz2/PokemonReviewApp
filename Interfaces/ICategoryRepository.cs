using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
	public interface ICategoryRepository
	{
		ICollection<Categories> GetCategories();
		Categories GetCategory(int id);
		ICollection<Pokemons> GetPokemonByCategory(int id);
		bool CategoryExists(int id);
	}
}
