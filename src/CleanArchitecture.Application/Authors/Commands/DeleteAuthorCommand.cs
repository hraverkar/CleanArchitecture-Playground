using CleanArchitecture.Application.Abstractions.Commands;
using CleanArchitecture.Application.Abstractions.Repositories;
using CleanArchitecture.Application.Weather.Commands;
using CleanArchitecture.Core.Abstractions.Guards;
using CleanArchitecture.Core.Locations.Entities;
using CleanArchitecture.Core.Weather.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Authors.Commands
{
    public sealed record DeleteAuthorCommand(Guid Id): Command;
    public sealed class DeleteAuthorCommandHandler : CommandHandler<DeleteAuthorCommand>
    {
        private readonly IRepository<Author> _repository;
        public DeleteAuthorCommandHandler(IRepository<Author> repository, IUnitOfWork unitOfWork): base(unitOfWork)
        {
            _repository = repository;
        }

        protected override async Task HandleAsync(DeleteAuthorCommand request)
        {
            var author = await _repository.GetByIdAsync(request.Id);
            author = Guard.Against.NotFound(author);
            await UnitOfWork.CommitAsync();
        }
    }
}