using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Login.Models
{
    public sealed class TokenDto
    {
        public string AccessToken { get; set; }
        public DateTime ExpireTime { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }

    }
}
