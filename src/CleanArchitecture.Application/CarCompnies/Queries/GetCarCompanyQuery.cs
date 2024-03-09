using AutoMapper;
using CleanArchitecture.Application.Abstractions.Queries;
using CleanArchitecture.Application.Abstractions.Repositories;
using CleanArchitecture.Application.CarCompnies.Models;
using CleanArchitecture.Core.CarCompanies.Entities;

namespace CleanArchitecture.Application.CarCompnies.Queries
{
    public sealed record GetCarCompanyQuery() : Query<List<CarCompaniesDto>>;
    public sealed class GetCompaniesQueryHandler : QueryHandler<GetCarCompanyQuery, List<CarCompaniesDto>>
    {
        private readonly IRepository<CarCompany> _repository;

        public GetCompaniesQueryHandler(IMapper mapper, IRepository<CarCompany> repository) : base(mapper)
        {
            _repository = repository;
        }

        protected async override Task<List<CarCompaniesDto>> HandleAsync(GetCarCompanyQuery request)
        {
            var carCompnies = _repository.GetAll(false).OrderBy(e => e.CarManufactureName);
            return Mapper.Map<List<CarCompaniesDto>>(carCompnies);
        }
    }
}
