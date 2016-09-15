using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceEntities;
using UnitOfWork;
using Repositories;

namespace GenericService
{
    public sealed class TaskService : AbstractService<DomainCore.DomainTask>, ITaskService
    {
        private IUnitOfWork uow;
        private IDomainTaskRepositorie repository;
        public TaskService(IUnitOfWork uow, IDomainTaskRepositorie repository) : base(uow, repository)
        {
                
        }
        public ServiceTask GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
