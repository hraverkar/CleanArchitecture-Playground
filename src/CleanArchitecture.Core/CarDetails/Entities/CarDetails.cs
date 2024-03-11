using CleanArchitecture.Core.Abstractions.Entities;
using CleanArchitecture.Core.CarCompanies.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.CarDetails.Entities
{
    public sealed class CarDetails : AggregateRoot
    {
        private CarDetails(CarCompany carCompany, string CarModelName, int manufactureYear) { }

    }
}
