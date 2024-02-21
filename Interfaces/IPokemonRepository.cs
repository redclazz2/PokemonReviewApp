using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
	public interface IPokemonRepository
	{
		ICollection<Pokemons> GetPokemons();
		Pokemons GetPokemon(int id);
		Pokemons GetPokemon(string name);
		decimal GetPokemonRating(int id);
		bool PokemonExists(int id);
	}
}
