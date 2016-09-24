using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DomainEF.Interfaces
{
    interface IGenericRepository<TEntity>: IDisposable //TODO: Entities may have common interface
        where TEntity : class
    {
        IQueryable<TEntity> All();
        IQueryable<TEntity> AllIncluding(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params Expression<Func<TEntity, object>>[] includeProperties);
        TEntity Find(int id);
        TEntity Find(string id);
        void Delete(int id);
        void Delete(TEntity entity);
        void Update(TEntity entity);
        void Insert(TEntity entity);
    }
}
