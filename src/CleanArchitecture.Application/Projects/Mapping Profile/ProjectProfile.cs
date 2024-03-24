using AutoMapper;
using CleanArchitecture.Application.Projects.Models;
using CleanArchitecture.Core.Projects.Entities;

namespace CleanArchitecture.Application.Projects.Mapping_Profile
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectResponseDto>()
                    .ForMember(dest => dest.Id, e => e.MapFrom(src => src.Id))
                    .ForMember(dest => dest.ProjectName, e => e.MapFrom(src => src.ProjectName))
                    .ForMember(dest => dest.ProjectDescription, e => e.MapFrom(src => src.ProjectDescription))
                    .ForMember(dest => dest.IsDeleted, e => e.MapFrom(src => src.IsDeleted))
                    .ForMember(dest => dest.CreatedAt, e => e.MapFrom(src => src.CreatedAt))
                    .ForMember(dest => dest.CreatedBy, e => e.MapFrom(src => src.CreatedBy))
                    .ForMember(dest => dest.UpdatedAt, e => e.MapFrom(src => src.UpdatedAt))
                    .ForMember(dest => dest.UpdatedBy, e => e.MapFrom(src => src.UpdatedBy));
        }
    }
}
