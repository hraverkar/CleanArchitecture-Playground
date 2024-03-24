using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Projects.Models
{
    public sealed class ProjectResponseListDto
    {
        public int count { get; set; }
        public List<ProjectResponseDto> items { get; set; }
    }
}
