using Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IService<TEntity> : IDisposable
        where TEntity: class
    {
        IEnumerable<TEntity> GetAll();
        OperationDetails Create(TEntity item);
        OperationDetails Update(TEntity item);
        OperationDetails Delete(TEntity item);
        TEntity Find(int id);
    }
}
