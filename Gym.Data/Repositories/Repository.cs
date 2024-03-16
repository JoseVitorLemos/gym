using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Gym.Data.DatabaseContext;
using Gym.Domain.Entities;
using Gym.Helpers.Enums;
using Gym.Helpers.Exceptions;
using Microsoft.EntityFrameworkCore.Query;
using Gym.Domain.Interfaces;

namespace Gym.Data.Repositories;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    public readonly DbSet<T> _dbSet;
    public readonly DataContext _dbContext;

    public Repository(DataContext dbContext)
    {
        _dbSet = dbContext.Set<T>();
        _dbContext = dbContext;
    }

    public async Task<bool> Insert(T entity)
    {
        try
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            throw new Exception($"Error on {_dbSet.GetType().Name}", e);
        }
    }

    public async Task<bool> InsertAll(List<T> entity)
    {
        try
        {
            await _dbSet.AddRangeAsync(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            throw new Exception($"Error on {_dbSet.GetType().Name}", e);
        }
    }

    public async Task<T> GetById(Guid id)
        => await _dbSet.FindAsync(id);

    public async Task<List<T>> GetAll(Expression<Func<T, bool>> filter = null,
            bool? paginate = true, int? page = 1, int? pageSize = 10)
    {
        var query = _dbSet.AsQueryable();

        if (paginate.Value)
            query = query.Skip((page.Value - 1) * pageSize.Value)
                         .Take(pageSize.Value);

        if (filter != null)
            query = query.Where(filter)
                         .AsNoTracking();

        return await query.ToListAsync();
    }

    public async Task<IQueryable<T>> FindByCondition(Expression<Func<T, bool>> filter,
            bool? paginate = false, int? page = 1, int? pageSize = 10)
    {
        var query = _dbSet.AsQueryable()
                        .Where(filter).AsNoTracking();

        if (paginate.Value)
            query = query.Skip((page.Value - 1) * pageSize.Value)
                         .Take(pageSize.Value);

        return (await query.ToListAsync()).AsQueryable();
    }

    public async Task<bool> Update(T entity)
    {
        try
        {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            throw new Exception($"Error on {_dbSet.GetType().Name}", e);
        }
    }

    public async Task<bool> UpdateAll(List<T> entities)
    {
        try
        {
            _dbSet.UpdateRange(entities);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            throw new Exception($"Error on {_dbSet.GetType().Name}", e);
        }
    }

    public async Task<bool> ExecuteUpdate(Expression<Func<T, bool>> where,
            Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> setPropertyCalls)
    {
        try
        {
            await _dbSet.Where(where)
                        .ExecuteUpdateAsync(setPropertyCalls);
            return true;
        }
        catch (Exception e)
        {
            throw new Exception($"Error on {_dbSet.GetType().Name}", e);
        }
    }

    public async Task<bool> Delete(Guid id)
    {
        try
        {
            var entity = await GetById(id);
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            throw new GlobalException(HttpStatusCodes.InternalServerError, $"Error on delete", e.InnerException);
        }
    }

    public async Task<bool> DeleteAll(List<T> entities)
    {
        try
        {
            _dbSet.RemoveRange(entities);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            throw new Exception($"Error on {_dbSet.GetType().Name}", e);
        }
    }

    public async Task<bool> ExecuteDelete(Expression<Func<T, bool>> where)
    {
        try
        {
            await _dbSet.Where(where).ExecuteDeleteAsync();
            return true;
        }
        catch (Exception e)
        {
            throw new Exception($"Error on {_dbSet.GetType().Name}", e);
        }
    }

    public async Task<bool> EnableOrDisable(Guid id, bool status)
    {
        try
        {
            var entity = await GetById(id);
            entity.AlterStatus(status);
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            throw new GlobalException(HttpStatusCodes.InternalServerError, $"Error on EnableOrDisable", e.InnerException);
        }
    }
}
