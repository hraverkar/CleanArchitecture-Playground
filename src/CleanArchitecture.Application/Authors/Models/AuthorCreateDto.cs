using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Authors.Models
{
    public sealed class AuthorCreateDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
    }
}
