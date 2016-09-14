using System.Collections.Generic;
using GenericRepository;
using DomainCore;

namespace Reposiraries.Interfaces
{
    public interface IDomainTaskRepositorie : IRepository<DomainTask>
    {
        IEnumerable<DomainTask> GetAllByPrioryty(Priority priority);
        IEnumerable<DomainTask> GetAllByStatus(Status status);
        //TODO: add functionality
    }
}
