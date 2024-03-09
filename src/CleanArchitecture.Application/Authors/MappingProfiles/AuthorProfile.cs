using AutoMapper;
using CleanArchitecture.Application.Authors.Models;
using CleanArchitecture.Application.Weather.Models;
using CleanArchitecture.Core.Locations.Entities;
using CleanArchitecture.Core.Weather.Entities;

namespace CleanArchitecture.Application.Authors.MappingProfiles
{
    public sealed class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author, AuthorDto>()
                    .ForMember(dest => dest.Id, e => e.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Name, e => e.MapFrom(src => src.Name))
                    .ForMember(dest => dest.Email, e => e.MapFrom(src => src.Email))
                    .ForMember(dest => dest.Description, e => e.MapFrom(src => src.Description))
                    .ForMember(dest => dest.IsDeleted, e => e.MapFrom(src => src.IsDeleted));

        }
    }
}
