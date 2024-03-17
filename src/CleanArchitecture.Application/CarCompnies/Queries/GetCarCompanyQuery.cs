using AutoMapper;
using CleanArchitecture.Application.Abstractions.Queries;
using CleanArchitecture.Application.Abstractions.Repositories;
using CleanArchitecture.Application.Abstractions.Services;
using CleanArchitecture.Application.CarCompnies.Models;
using CleanArchitecture.Core.CarCompanies.Entities;

namespace CleanArchitecture.Application.CarCompnies.Queries
{
    public sealed record GetCarCompanyQuery() : Query<List<CarCompaniesDto>>;
    public sealed class GetCompaniesQueryHandler : QueryHandler<GetCarCompanyQuery, List<CarCompaniesDto>>
    {
        private readonly IRepository<CarCompany> _repository;
        private readonly IEmailNotificationService _emailNotificationService;

        public GetCompaniesQueryHandler(IMapper mapper, IRepository<CarCompany> repository, IEmailNotificationService emailNotificationService) : base(mapper)
        {
            _repository = repository;
            _emailNotificationService = emailNotificationService;
        }

        protected async override Task<List<CarCompaniesDto>> HandleAsync(GetCarCompanyQuery request)
        {
            var carCompnies = _repository.GetAll(false).OrderBy(e => e.CarManufactureName);
            _emailNotificationService.EmailNotificationAlertAsync("harshal", "raverkar", "khandwa");
            return Mapper.Map<List<CarCompaniesDto>>(carCompnies);
        }
    }
}
