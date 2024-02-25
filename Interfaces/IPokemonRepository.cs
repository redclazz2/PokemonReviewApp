using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
	public interface IPokemonRepository
	{
		ICollection<Pokemon> GetPokemons();
		Pokemon GetPokemon(int id);
		Pokemon GetPokemon(string name);
		decimal GetPokemonRating(int id);
		bool PokemonExists(int id);
		bool CreatePokemon(int categoryId, int ownerId, Pokemon pokemon);
		bool Save();
	}
}
