using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
	public interface IOwnerRepository
	{
		ICollection<Owner> GetOwners();
		Owner GetOwner(int id);
		ICollection<Owner> GetOwnersOfAPokemon(int pokemonId);
		ICollection<Pokemon> GetPokemonByOwner(int ownerId);
		bool OwnerExists(int ownerId);

		bool CreateOwner(Owner owner);
		bool Save();
	}
}
