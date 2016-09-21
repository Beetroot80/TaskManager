using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IEntityRepository<T> : IDisposable
    {
        IEnumerable<T> All();
        IEnumerable<T> AllIncluding(params Expression<Func<T, object>> [] includeProperties);
        T Find(int id);
        void InsertOrUpdate(T entity);
        void Delete(int id);
    }
}
