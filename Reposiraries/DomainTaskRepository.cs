using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DomainCore;
using GenericRepository;
using Reposiraries.Interfaces;

namespace Reposiraries
{
    class DomainTaskRepository : Repository<DomainTask>, IDomainTaskRepositorie
    {
        public DomainTaskRepository(DbContext context) : base(context)
        {                
        }
        public IEnumerable<DomainTask> GetAllByPrioryty(Priority priority)
        {
            return set.Where(x => x.Priority == priority).AsEnumerable();
        }

        public IEnumerable<DomainTask> GetAllByStatus(Status status)
        {
            return set.Where(x => x.Status == status).Include("Priority")
                .Include("Status")
                .Include("CreatedBy")
                .AsEnumerable();
        }
    }
}
