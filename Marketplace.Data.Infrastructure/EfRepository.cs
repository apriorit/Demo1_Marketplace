using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Marketplace.Contracts;
using Marketplace.Contracts.Models;
using Marketplace.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Marketplace.Data.Infrastructure
{
    public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class, IDeletable
    {
        protected readonly DbContext _context;
        private readonly IEntityConverter _entityConverter;

        public EfRepository(DbContext context, IEntityConverter entityConverter)
        {
            _context = context;
            _entityConverter = entityConverter;
        }

        public virtual async Task<int> CreateAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message.Contains("The INSERT statement conflicted with the FOREIGN KEY constraint"))
                {
                    throw new NotFoundException(ex.Message);
                }
                throw;
            }
        }

        public virtual IQueryable<TEntity> Get()
        {
            return _context.Set<TEntity>().Where(x => x.IsActive == true);
        }

        public async Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true, bool ignoreQueryFilters = false)
        {
            IQueryable<TEntity> query = Get();

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (ignoreQueryFilters)
            {
                query = query.IgnoreQueryFilters();
            }
            if (orderBy != null)
            {
                return await orderBy(query).ToListAsyncExt();
            }
            else
            {
                return await query.ToListAsyncExt();
            }
        }

        public async Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true, bool ignoreQueryFilters = false)
        {
            IQueryable<TEntity> query = Get();

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (ignoreQueryFilters)
            {
                query = query.IgnoreQueryFilters();
            }

            if (orderBy != null)
            {
                return await orderBy(query).FirstOrDefaultAsync();
            }
            else
            {
                return await query.FirstOrDefaultAsync();
            }
        }

        public Task<int> UpdateAsync(Expression<Func<TEntity, bool>> itemsToUpdate, TEntity value)
        {
            if (_context.Entry(value).State == Microsoft.EntityFrameworkCore.EntityState.Detached)
            {
                _context.Set<TEntity>().Where(itemsToUpdate).ToList().All(item =>
                {
                    _context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                    _entityConverter.AssignTo<TEntity, TEntity>(value, ref item);
                    _context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                    return true;
                });
            }

            return _context.SaveChangesAsync();
        }

        public Task<int> DeleteAsync(TEntity value)
        {
            value.IsActive = false;
            return _context.SaveChangesAsync();
        }

        public Task<int> DeleteAsync(Expression<Func<TEntity, bool>> itemsToDelete)
        {
            var toModify = _context.Set<TEntity>().Where(itemsToDelete);
            foreach (var item in toModify)
            {
                item.IsActive = false;
            }
            _context.Set<TEntity>().UpdateRange(toModify);
            return _context.SaveChangesAsync();
        }

        public Task<int> CreateRangeAsync(IEnumerable<TEntity> entity)
        {
            _context.Set<TEntity>().AddRangeAsync(entity);
            return _context.SaveChangesAsync();
        }

        public async Task WithTransactionAsync(Func<Task> callback)
        {
            using (var tran = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await callback();
                    await tran.CommitAsync();
                }
                catch
                {
                    await tran.RollbackAsync();
                    throw;
                }
            }
        }

        public Task<TStore[]> ExecuteStoreProcedureAsync<TStore>(string script) where TStore : class
        {
            return _context.Set<TStore>().FromSqlRaw(script).ToArrayAsync();
        }

        public Task<int> HardDeleteAsync(TEntity value)
        {
            _context.Set<TEntity>().Remove(value);
            return _context.SaveChangesAsync();
        }

        public Task<int> HardDeleteAsync(Expression<Func<TEntity, bool>> itemsToDelete)
        {
            _context.Set<TEntity>().RemoveRange(_context.Set<TEntity>().Where(itemsToDelete));
            return _context.SaveChangesAsync();
        }
    }
}
