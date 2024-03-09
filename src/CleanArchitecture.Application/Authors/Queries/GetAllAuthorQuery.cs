using AutoMapper;
using CleanArchitecture.Application.Abstractions.Queries;
using CleanArchitecture.Application.Abstractions.Repositories;
using CleanArchitecture.Application.Authors.Models;
using CleanArchitecture.Core.Abstractions.Guards;
using CleanArchitecture.Core.Locations.Entities;

namespace CleanArchitecture.Application.Authors.Queries
{
    public sealed record GetAllAuthorQuery() : Query<List<AuthorDto>>;
    public sealed class GetAllAuthorQueryHandler : QueryHandler<GetAllAuthorQuery, List<AuthorDto>>
    {
        private readonly IRepository<Author> _repository;
        public GetAllAuthorQueryHandler(IMapper mapper, IRepository<Author> repository) : base(mapper)
        {

            _repository = repository;
        }

        protected async override Task<List<AuthorDto>> HandleAsync(GetAllAuthorQuery request)
        {
            var author = _repository.GetAll(false);
            Guard.Against.NotFound(author);
            return Mapper.Map<List<AuthorDto>>(author);
        }
    }
}
