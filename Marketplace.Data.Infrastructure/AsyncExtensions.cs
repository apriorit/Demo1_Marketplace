using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Marketplace.Data.Infrastructure
{
    public static class AsyncExtensions
    {
        public static Task<TEntity> FirstOrDefaultAsync<TEntity>(this IQueryable<TEntity> query, Expression<Func<TEntity, bool>> selector) where TEntity : class
        {
            switch (query)
            {
                case DbSet<TEntity> dbset:
                    {
                        return EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(dbset, selector);
                    }
                default: throw new NotSupportedException();
            }
        }

        public static Task<List<TEntity>> ToListAsyncExt<TEntity>(this IQueryable<TEntity> query) where TEntity : class
        {
            switch (query)
            {
                case DbSet<TEntity> dbset:
                    {
                        return EntityFrameworkQueryableExtensions.ToListAsync(dbset);
                    }
                default:
                    return query.ToListAsync();
            }
        }
    }
}
