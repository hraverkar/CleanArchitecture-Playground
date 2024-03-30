using CleanArchitecture.Application.Abstractions.Commands;
using CleanArchitecture.Application.Abstractions.Repositories;
using CleanArchitecture.Core.Abstractions.Guards;
using CleanArchitecture.Core.CarCompanies.Entities;

namespace CleanArchitecture.Application.CarCompnies.Commands
{
    public sealed record DeleteCarCompanyCommad(Guid Id) : Command;
    public sealed class DeleteCarCompanyCommadHandler : CommandHandler<DeleteCarCompanyCommad>
    {
        public readonly IRepository<CarCompany> _repository;
        public DeleteCarCompanyCommadHandler(IRepository<CarCompany> repository, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _repository = repository;
        }

        protected async override Task HandleAsync(DeleteCarCompanyCommad request)
        {
            var carCompany = await _repository.GetByIdAsync(request.Id);
            carCompany = Guard.Against.NotFound(carCompany);
            _repository.SoftDelete(carCompany);
            await UnitOfWork.CommitAsync();
        }
    }
}