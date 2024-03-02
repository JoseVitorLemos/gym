using Gym.Data.DatabaseContext;
using Gym.Data.Repositories;
using Gym.Domain.Entities;
using Gym.Domain.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace Gym.Data.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    public readonly DataContext _context;
    private IDbContextTransaction _transaction;
    private Dictionary<Type, object> _repositories;

    public UnitOfWork(DataContext context)
    {
        _context = context;
        _repositories = new Dictionary<Type, object>();
    }

    public IRepository<T> GetRepository<T>() where T : BaseEntity
    {
        if (_repositories.ContainsKey(typeof(T)))
            return (IRepository<T>)_repositories[typeof(T)];

        var repository = new Repository<T>(_context);
        _repositories.Add(typeof(T), repository);
        return repository;
    }

    public async Task BeginTransactionAsync()
        => _transaction = await _context.Database.BeginTransactionAsync();

    public async Task CommitAsync()
        => await _transaction.CommitAsync();

    public async Task RollbackAsync()
        => await _transaction.RollbackAsync();

    public async ValueTask DisposeAsync()
        => await _context.DisposeAsync();
}
