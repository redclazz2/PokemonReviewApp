using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
	public interface IOwnerRepository
	{
		ICollection<Owners> GetOwners();
		Owners GetOwner(int id);
		ICollection<Owners> GetOwnersOfAPokemon(int pokemonId);
		ICollection<Pokemons> GetPokemonByOwner(int ownerId);
		bool OwnerExists(int ownerId);
	}
}
