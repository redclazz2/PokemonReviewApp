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

		public bool CreateOwner(Owner owner)
		{
			_context.Add(owner);
			return Save();
		}

		public Owner GetOwner(int id)
		{
			return _context.Owners.Where(x => x.Id == id).FirstOrDefault();
		}

		public ICollection<Owner> GetOwners()
		{
			return _context.Owners.OrderBy(x => x.Id).ToList();
		}

		public ICollection<Owner> GetOwnersOfAPokemon(int pokemonId)
		{
			return _context.PokemonOwners.Where(p => p.PokemonId == pokemonId).Select(o => o.Owner).ToList();
		}

		public ICollection<Pokemon> GetPokemonByOwner(int ownerId)
		{
			return _context.PokemonOwners.Where(o => o.OwnerId == ownerId).Select(p => p.Pokemon).ToList();
		}

		public bool OwnerExists(int ownerId)
		{
			return _context.Owners.Any(x => x.Id == ownerId);
		}

		public bool Save()
		{
			var _saved = _context.SaveChanges();
			return _saved > 0 ? true : false;
		}

		public bool UpdateOwner(Owner owner)
		{
			_context.Update(owner);
			return Save();
		}
	}
}
