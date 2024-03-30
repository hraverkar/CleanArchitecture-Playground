using AutoMapper;
using CleanArchitecture.Application.Authors.Models;
using CleanArchitecture.Application.CarCompnies.Models;
using CleanArchitecture.Application.Task_Details.Models;
using CleanArchitecture.Core.CarCompanies.Entities;
using CleanArchitecture.Core.Locations.Entities;
using CleanArchitecture.Core.Task.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Task_Details.Mapping_Profiles
{
    public sealed class TaskDetailsProfile : Profile
    {
        public TaskDetailsProfile()
        {
            CreateMap<TaskDetails, TaskDetailsResponseDto>()
                    .ForMember(dest => dest.Id, e => e.MapFrom(src => src.Id))
                    .ForMember(dest => dest.TaskTitle, e => e.MapFrom(src => src.TaskTitle))
                    .ForMember(dest => dest.TaskDetail, e => e.MapFrom(src => src.TaskDetail))
                    .ForMember(dest => dest.TaskStatusId, e => e.MapFrom(src => src.TaskStatus.Id))
                    .ForMember(dest => dest.TaskStatus, e => e.MapFrom(src => src.TaskStatus))
                    .ForMember(dest => dest.TaskAssignTo, e => e.MapFrom(src => src.TaskAssignTo))
                    .ForMember(dest => dest.TaskCreatedAt, e => e.MapFrom(src => src.TaskCreatedAt))
                    .ForMember(dest => dest.TaskCreatedBy, e => e.MapFrom(src => src.TaskCreatedBy))
                    .ForMember(dest => dest.IsDeleted, e => e.MapFrom(src => src.IsDeleted))
                       .ForMember(dest => dest.ProjectId, e => e.MapFrom(src => src.ProjectId))
                          .ForMember(dest => dest.Project, e => e.MapFrom(src => src.Project));

        }
    }
}
