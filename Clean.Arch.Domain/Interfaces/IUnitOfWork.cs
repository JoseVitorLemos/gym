using Clean.Arch.Domain.Entities;

namespace Clean.Arch.Domain.Interfaces;

public interface IUnitOfWork : IAsyncDisposable
{
    IRepository<T> GetRepository<T>() where T : BaseEntity;
    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
    new ValueTask DisposeAsync();
}
