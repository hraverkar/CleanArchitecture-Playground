using CleanArchitecture.Application.Abstractions.Commands;
using CleanArchitecture.Application.Abstractions.Repositories;
using CleanArchitecture.Core.CarCompanies.Entities;

namespace CleanArchitecture.Application.CarCompnies.Commands
{
    public sealed record CreateCarCompanyCommand(string CarManufactureCompany) : CreateCommand;
    public sealed class CreateCarCompanyCommandHandler : CreateCommandHandler<CreateCarCompanyCommand>
    {
        public readonly IRepository<CarCompany> _repository;
        public CreateCarCompanyCommandHandler(IRepository<CarCompany> repository, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _repository = repository;
        }

        protected async override Task<Guid> HandleAsync(CreateCarCompanyCommand request)
        {
            var carAvailable = _repository.GetAll(false).Any(t => t.CarManufactureName.ToLower() == request.CarManufactureCompany.ToLower());
            if (!carAvailable)
            {
                var carCompanyManufacture = CarCompany.Create(request.CarManufactureCompany);
                _repository.Insert(carCompanyManufacture);
                await UnitOfWork.CommitAsync();
                return carCompanyManufacture.Id;
            }
            return Guid.Empty;
        }
    }
}