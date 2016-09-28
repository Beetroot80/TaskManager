using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainEF.Interfaces;
using DomainEF.UOW;
using DomainEF.Repositories;
using UnitOfWork;
using DomainEF;
using ServiceEntities;
using AutoMapper;
using ServiceMapper;

namespace Services.Services
{
    public class InfoService
    {
        private UnitOfWork<TaskManagerContext> uow;

        public List<Status> StatusList()
        {
            using (uow = new UnitOfWork<TaskManagerContext>())
            {
                var domainStatus = uow.StatusRepo.All();
                return Mapper.Map<List<Status>>(domainStatus);
            }
        }
        public List<Priority> PriorityList()
        {
            using (uow = new UnitOfWork<TaskManagerContext>())
            {
                var domainPriority = uow.PriorityRepo.All();
                return Mapper.Map<List<Priority>>(domainPriority);
            }
        }
    }
}
