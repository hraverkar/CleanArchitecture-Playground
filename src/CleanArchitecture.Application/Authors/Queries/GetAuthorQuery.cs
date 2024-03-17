using AutoMapper;
using CleanArchitecture.Application.Abstractions.Queries;
using CleanArchitecture.Application.Abstractions.Repositories;
using CleanArchitecture.Application.Authors.Models;
using CleanArchitecture.Core.Abstractions.Guards;
using CleanArchitecture.Core.Locations.Entities;

namespace CleanArchitecture.Application.Authors.Queries
{
    public sealed record GetAuthorQuery(Guid Id) : Query<AuthorDto>;
    public sealed class GetAuthorQueryHandler : QueryHandler<GetAuthorQuery, AuthorDto>
    {
        private readonly IRepository<Author> _repository;
        public GetAuthorQueryHandler(IMapper mapper, IRepository<Author> repository) : base(mapper)
        {

            _repository = repository;
        }
        protected async override Task<AuthorDto> HandleAsync(GetAuthorQuery request)
        {
            var author = _repository.GetByIdAsync(request.Id);
            _ = Guard.Against.NotFound(author);
            return Mapper.Map<AuthorDto>(author);
        }
    }
}
