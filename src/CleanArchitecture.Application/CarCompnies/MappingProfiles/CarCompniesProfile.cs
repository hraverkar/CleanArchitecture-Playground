using AutoMapper;
using CleanArchitecture.Application.Authors.Models;
using CleanArchitecture.Application.CarCompnies.Models;
using CleanArchitecture.Core.CarCompanies.Entities;
using CleanArchitecture.Core.Locations.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
