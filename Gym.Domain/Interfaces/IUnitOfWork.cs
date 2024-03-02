using Gym.Domain.Entities;

namespace Gym.Domain.Interfaces;

public interface IUnitOfWork : IAsyncDisposable
{
    IRepository<T> GetRepository<T>() where T : BaseEntity;
    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
    new ValueTask DisposeAsync();
}
