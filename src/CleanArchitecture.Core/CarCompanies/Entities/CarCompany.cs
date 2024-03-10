using CleanArchitecture.Core.Abstractions.Entities;
using CleanArchitecture.Core.Abstractions.Guards;
using CleanArchitecture.Core.Locations.Entities;
using CleanArchitecture.Core.Locations.ValueObjects;

namespace CleanArchitecture.Core.CarCompanies.Entities
{
    public sealed class CarCompany : AggregateRoot
    {
        private CarCompany(string carManufactureName)
        {
            this.CarManufactureName = carManufactureName;
        }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private CarCompany() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public string CarManufactureName { get; set; }
        public static CarCompany Create(string carManufactureName)
        {
            carManufactureName = (carManufactureName ?? string.Empty).Trim();
            Guard.Against.NullOrEmpty(carManufactureName, nameof(CarManufactureName));
            return new CarCompany(carManufactureName);
        }
    }
}
