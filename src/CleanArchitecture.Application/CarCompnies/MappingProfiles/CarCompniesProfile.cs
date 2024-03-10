using AutoMapper;
using CleanArchitecture.Application.CarCompnies.Models;
using CleanArchitecture.Core.CarCompanies.Entities;

namespace CleanArchitecture.Application.CarCompnies.MappingProfiles
{
    public sealed class CarCompniesProfile : Profile
    {
        public CarCompniesProfile()
        {
            CreateMap<CarCompany, CarCompaniesDto>()
                .ForMember(dest => dest.Id, e => e.MapFrom(src => src.Id))
                .ForMember(dest => dest.CarManufactureName, e => e.MapFrom(src => src.CarManufactureName));
        }
    }
}
