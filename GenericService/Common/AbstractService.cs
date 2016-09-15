using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories;
using UnitOfWork;

namespace GenericService
{
    public abstract class AbstractService<T> : IService<T> where T : class
    {
        protected IUnitOfWork uow;
        protected IRepository<T> repository;

        public AbstractService(IUnitOfWork uow, IRepository<T> repository)
        {
            this.uow = uow;
            this.repository = repository;
        }

        //TODO: discover how to write exception messeges right
        public virtual void Create(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Entity contains null");
            repository.Insert(entity);
            uow.SaveChanges();
        }

        public void Delete(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Entity contained null");
            repository.Delete(entity);
            uow.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return repository.GetAll();
        }

        public void Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Entity contained null");
            repository.Update(entity);
            uow.SaveChanges();
        }
    }
}
