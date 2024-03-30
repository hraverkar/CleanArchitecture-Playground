using CleanArchitecture.Application.Projects.Models;
using CleanArchitecture.Application.Task_Status.Model;
using CleanArchitecture.Core.Task_Details.Task_Status_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskStatus = CleanArchitecture.Core.Task_Details.Task_Status_Entities.TaskStatus;

namespace CleanArchitecture.Application.Task_Details.Models
{
    public sealed class TaskDetailsResponseDto
    {
        public Guid Id { get; set; }
        public string TaskTitle { get; set; }
        public string TaskDetail { get; set; }
        public string TaskAssignTo { get; set; }
        public Guid TaskStatusId { get; set; }
        public TaskStatusResponseDto TaskStatus { get; set; }
        public DateTime TaskCreatedAt { get; set; }
        public string TaskCreatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public Guid ProjectId { get; set; }
        public ProjectResponseDto Project { get; set; }
    }
}
