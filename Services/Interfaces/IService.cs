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
        /// <summary>
        /// Add instance of given type to data source
        /// </summary>
        /// <returns>Information about operation results</returns>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="ArgumentException"></exception>
        OperationDetails Create(TEntity item);
        /// <summary>
        /// Update all or changed fields of item in data source
        /// </summary>
        /// <returns>Information about operation results</returns>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="System.Data.Entity.Validation.DbEntityValidationException"></exception>
        OperationDetails Update(TEntity item);
        /// <summary>
        /// Update all or changed fields of item in data source
        /// </summary>
        /// <returns>Information about operation results</returns>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="ArgumentException"></exception>
        OperationDetails Delete(TEntity item);
        TEntity Find(int id);
    }
}
