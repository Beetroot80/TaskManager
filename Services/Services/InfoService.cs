using System.Collections.Generic;
using ServiceEntities;
using AutoMapper;

using DomainEF.UnitOfWork;

namespace Services.Services
{
    public class InfoService
    {
        private UnitOfWork uow;

        public List<Status> StatusList()
        {
            using (uow = new UnitOfWork())
            {
                var domainStatus = uow.StatusRepo.GetAll();
                return Mapper.Map<List<Status>>(domainStatus);
            }
        }
        public List<Priority> PriorityList()
        {
            using (uow = new UnitOfWork())
            {
                var domainPriority = uow.PriorityRepo.GetAll();
                return Mapper.Map<List<Priority>>(domainPriority);
            }
        }
    }
}
