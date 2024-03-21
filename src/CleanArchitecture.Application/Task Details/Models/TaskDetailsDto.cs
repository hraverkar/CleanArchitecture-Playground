using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Task_Details.Models
{
    public sealed class TaskDetailsDto
    {
        public int count { get; set; } = 0;
        public List<TaskDetailsResponseDto> items { get; set; }
    }
}
