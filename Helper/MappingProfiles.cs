using AutoMapper;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Helper
{
	public class MappingProfiles : Profile
	{
        public MappingProfiles()
        {
            CreateMap<Pokemon,PokemonDto>();

            CreateMap<Category,CategoryDto>();
			CreateMap<CategoryDto, Category>();

			CreateMap<Country,CountryDto>();
			CreateMap<CountryDto, Country>();

			CreateMap<Owner,OwnerDto>();
            CreateMap<OwnerDto, Owner>();

            CreateMap<Review,ReviewDto>();
            CreateMap<Reviewer, ReviewerDto>();
        }
    }
}
