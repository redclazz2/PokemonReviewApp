using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
	public class CountryRepository : ICountryRepository
	{
		private DataContext _context;

        public CountryRepository(DataContext context)
        {
            this._context = context;
        }

        public bool CountryExists(int countryId)
		{
			return _context.Countries.Any(c=> c.Id == countryId);
		}

		public bool CreateCountry(Country country)
		{
			_context.Add(country);
			return Save();
		}

		public ICollection<Country> GetCountries()
		{
			return _context.Countries.OrderBy(c => c.Id).ToList();
		}

		public Country GetCountry(int id)
		{
			return _context.Countries.Where(c => c.Id == id).FirstOrDefault();
		}

		public Country GetCountryByOwner(int ownerId)
		{
			return _context.Owners.Where(p => p.Id == ownerId).Select(c => c.Country).FirstOrDefault();
		}

		public ICollection<Owner> GetOwnersFromACountry(int countryId)
		{
			//This part is special, notice how in the where clause we dive into the country ID.
			//This is important so we can properly query the owner data from the country id.
			return _context.Owners.Where(c => c.Country.Id == countryId).ToList();
		}

		public bool Save()
		{
			var _saved = _context.SaveChanges();
			return _saved > 0 ? true : false;
		}
	}
}
