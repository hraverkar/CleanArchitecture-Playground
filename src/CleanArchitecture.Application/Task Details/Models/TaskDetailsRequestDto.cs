using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Task_Details.Models
{
    public sealed class TaskDetailsRequestDto
    {
        public string TaskTitle { get; set; }
        public string TaskDetail { get; set; }
        public string TaskAssignTo { get; set; }
        public string TaskStatus { get; set; }
        public DateTime TaskCreatedAt { get; set; }
        public string TaskCreatedBy { get; set; }
    }
}
