
using CleanArchitecture.Core.Abstractions.Entities;

namespace CleanArchitecture.Application.Abstractions.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> CommitAsync(CancellationToken cancellationToken = default);
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : AggregateRoot;
    }
}
