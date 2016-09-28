using System;
using System.Linq;
using System.Linq.Expressions;

namespace DomainEF.Interfaces
{
    interface IGenericRepository<TEntity> : IDisposable
        where TEntity : class
    {
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAllIncluding(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params Expression<Func<TEntity, object>> [] includeProperties);

        TEntity Find(int id);
        TEntity Find(string id);

        void Delete(int id);
        void Delete(string id);
        void Delete(TEntity entity);

        void Update(TEntity entity);
        void Insert(TEntity entity);
    }
}
