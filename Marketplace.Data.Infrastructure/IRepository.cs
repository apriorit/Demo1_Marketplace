using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Marketplace.Data.Infrastructure
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Get();
        Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true, bool ignoreQueryFilters = false);
        Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true, bool ignoreQueryFilters = false);
        Task<int> CreateAsync(TEntity entity);
        Task<int> UpdateAsync(Expression<Func<TEntity, bool>> itemsToUpdate, TEntity value);
        Task<int> DeleteAsync(TEntity value);
        Task<int> DeleteAsync(Expression<Func<TEntity, bool>> itemsToDelete);
        Task<int> HardDeleteAsync(TEntity value);
        Task<int> HardDeleteAsync(Expression<Func<TEntity, bool>> itemsToDelete);
        Task<int> CreateRangeAsync(IEnumerable<TEntity> entity);
        Task WithTransactionAsync(Func<Task> callback);
        Task<TStore[]> ExecuteStoreProcedureAsync<TStore>(string script) where TStore : class;
    }
}
