using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
	public interface ICountryRepository
	{
		ICollection<Countries> GetCountries();
		Countries GetCountry(int id);
		Countries GetCountryByOwner(int ownerId);
		ICollection<Owners> GetOwnersFromACountry(int countryId);
		bool CountryExists(int countryId);
	}
}
