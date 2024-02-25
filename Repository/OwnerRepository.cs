using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
	public class OwnerRepository : IOwnerRepository
	{
		private DataContext _context;
		
		public OwnerRepository(DataContext context) {
			this._context = context;
		}

		public Owners GetOwner(int id)
		{
			return _context.Owners.Where(x => x.Id == id).FirstOrDefault();
		}

		public ICollection<Owners> GetOwners()
		{
			return _context.Owners.OrderBy(x => x.Id).ToList();
		}

		public ICollection<Owners> GetOwnersOfAPokemon(int pokemonId)
		{
			return _context.PokemonOwners.Where(p => p.PokemonId == pokemonId).Select(o => o.Owner).ToList();
		}

		public ICollection<Pokemons> GetPokemonByOwner(int ownerId)
		{
			return _context.PokemonOwners.Where(o => o.OwnerId == ownerId).Select(p => p.Pokemon).ToList();
		}

		public bool OwnerExists(int ownerId)
		{
			return _context.Owners.Any(x => x.Id == ownerId);
		}
	}
}
