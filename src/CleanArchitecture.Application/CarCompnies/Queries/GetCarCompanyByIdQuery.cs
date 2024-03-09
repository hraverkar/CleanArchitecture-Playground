using AutoMapper;
using CleanArchitecture.Application.Abstractions.Queries;
using CleanArchitecture.Application.Abstractions.Repositories;
using CleanArchitecture.Application.CarCompnies.Models;
using CleanArchitecture.Core.CarCompanies.Entities;

namespace CleanArchitecture.Application.CarCompnies.Queries
{
    public sealed record GetCarCompanyByIdQuery(Guid id) : Query<CarCompaniesDto>;
    public sealed class GetCarCompanyByIdQueryHandler : QueryHandler<GetCarCompanyByIdQuery, CarCompaniesDto>
    {
        private readonly IRepository<CarCompany> _repository;

        public GetCarCompanyByIdQueryHandler(IMapper mapper, IRepository<CarCompany> repository) : base(mapper)
        {
            _repository = repository;
        }

        protected async override Task<CarCompaniesDto> HandleAsync(GetCarCompanyByIdQuery request)
        {
            var carCompnies = _repository.GetByIdAsync(request.id);
            return Mapper.Map<CarCompaniesDto>(carCompnies);
        }
    }
}
