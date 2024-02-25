using AutoMapper;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Helper
{
	public class MappingProfiles : Profile
	{
        public MappingProfiles()
        {
            CreateMap<Pokemons,PokemonDto>();
            CreateMap<Categories,CategoryDto>();
            CreateMap<Countries,CountryDto>();
            CreateMap<Owners,OwnerDto>();
            CreateMap<Reviews,ReviewDto>();
            CreateMap<Reviewers, ReviewerDto>();
        }
    }
}
