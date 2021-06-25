using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ToDo.Core.Repositories;
using ToDo.Entities;

namespace ToDo.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext context;

        public Repository(DbContext context)
        {
            this.context = context;
        }
        public virtual async Task AddAsync(TEntity entity)
        {
            await context.Set<TEntity>().AddAsync(entity);
        }

        public virtual IQueryable<TEntity> AsNoTracking()
        {
            return context.Set<TEntity>().AsNoTracking();
        }

        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await context.Set<TEntity>().AddRangeAsync(entities);
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return context.Set<TEntity>().Where(predicate);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Pagination pagination)
        {
            return await context.Set<TEntity>().ToListAsync();
        }
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await context.Set<TEntity>().ToListAsync();
        }

        public virtual ValueTask<TEntity> GetByIdAsync(int id)
        {
            return context.Set<TEntity>().FindAsync(id);
        }
        
        public virtual Task<TEntity> GetByIdAsNoTrackingAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var entity = context.Set<TEntity>().AsNoTracking().Where(predicate).SingleOrDefaultAsync();            
            return entity;
        }

        public virtual void Remove(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            context.Set<TEntity>().RemoveRange(entities);
        }

        public Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {          
            return context.Set<TEntity>().SingleOrDefaultAsync(predicate);
        }
        
        public virtual Task<int> CountAsync()
        {
            return context.Set<TEntity>().CountAsync();
        }
    }
}
