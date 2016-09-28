using System;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;

using DomainEF.Interfaces;

namespace DomainEF.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class
    {
        private TaskManagerContext context;
        private DbSet<TEntity> dbSet;

        internal TaskManagerContext Context
        {
            get
            {
                return context;
            }
        }
        internal DbSet<TEntity> DbSet
        {
            get
            {
                return dbSet;
            }
        }

        public GenericRepository(ITaskManagerContext context)
        {
            this.context = context as TaskManagerContext;
            dbSet = this.Context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAll()
        {
            return dbSet.AsQueryable();
        }

        public IQueryable<TEntity> GetAllIncluding(Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           params Expression<Func<TEntity, object>> [] includeProperties)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).AsQueryable();
            }
            else
            {
                return query.AsQueryable();
            }

        }

        public void Delete(int id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }
        public void Delete(string id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public TEntity Find(int id)
        {
            return dbSet.Find(id);
        }
        public TEntity Find(string id)
        {
            return dbSet.Find(id);
        }

        public void Insert(TEntity entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Added;
        }

        public void Update(TEntity entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }
    }

}

