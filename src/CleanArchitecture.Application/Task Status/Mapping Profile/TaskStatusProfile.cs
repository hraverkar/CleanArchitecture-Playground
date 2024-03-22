using AutoMapper;
using CleanArchitecture.Application.Task_Status.Model;
using CleanArchitecture.Core.Task_Details.Task_Status_Entities;
using TaskStatus = CleanArchitecture.Core.Task_Details.Task_Status_Entities.TaskStatus;

namespace CleanArchitecture.Application.Task_Details.Mapping_Profiles
{
    public sealed class TaskStatusProfile : Profile
    {
        public TaskStatusProfile()
        {
            CreateMap<TaskStatus, TaskStatusResponseDto>()
                    .ForMember(dest => dest.Id, e => e.MapFrom(src => src.Id))
                    .ForMember(dest => dest.StatusName, e => e.MapFrom(src => src.StatusName))
                    .ForMember(dest => dest.CreatedAt, e => e.MapFrom(src => src.CreatedAt))
                    .ForMember(dest => dest.CreatedBy, e => e.MapFrom(src => src.CreatedBy))
                    .ForMember(dest => dest.IsDeleted, e => e.MapFrom(src => src.IsDeleted));
        }
    }
}
