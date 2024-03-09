using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.CarCompnies.Models
{
    public sealed class CarCompaniesDto
    {
        public Guid Id { get; set; }
        public string CarManufactureName { get; set; }
    }
}
