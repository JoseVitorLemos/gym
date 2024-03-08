using System.Linq.Expressions;
using Gym.Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Gym.Domain.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    Task<bool> Insert(T entity);
    Task<bool> InsertAll(List<T> entity);
    Task<T> GetById(Guid id);
    Task<List<T>> GetAll(Expression<Func<T, bool>> filter = null, 
            bool? paginate = true, int? page = 1, int? pageSize = 10);
    Task<IQueryable<T>> FindByCondition(Expression<Func<T, bool>> filter, 
            bool? paginate = false, int? page = 1, int? pageSize = 10);
    Task<bool> Update(T entity);
    Task<bool> UpdateAll(List<T> entities);
    Task<bool> ExecuteUpdate(Expression<Func<T, bool>> filter, 
            Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> setPropertyCalls);
    Task<bool> Delete(Guid id);
    Task<bool> DeleteAll(List<T> entities);
    Task<bool> EnableOrDisable(Guid id, bool status);
}
