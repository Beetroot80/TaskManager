using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Repositories.Common
{
    //TODO: Right way to delete, update, insert... 
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected DbContext context;
        protected DbSet<T> set;

        public Repository(DbContext context)
        {
            this.context = context;
            set = context.Set<T>();
        }

        public virtual void Delete(T entity)
        {
            set.Remove(entity);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return set.AsEnumerable();
        }

        public IEnumerable<T> GetAllWithPredicate(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null)
                return this.GetAll();
            else
                return set.Where(predicate).AsEnumerable();
        }

        public virtual T GetById(int id)
        {
            return set.Find(id);
        }

        public virtual void Insert(T entity)
        {
            context.Entry<T>(entity).State = EntityState.Added;
        }

        public virtual void Update(T entity)
        {
            context.Entry<T>(entity).State = EntityState.Modified;
        }
    }
}
