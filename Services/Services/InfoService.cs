using System.Collections.Generic;
using ServiceEntities;
using AutoMapper;

using DomainEF.UnitOfWork;
using System.Linq;
using System;

namespace Services.Services
{
    public class StatusService
    {
        private UnitOfWork uow;

        public List<Status> StatusList()
        {
            using (uow = new UnitOfWork())
            {
                var domainStatus = uow.StatusRepo.GetAll().ToList();
                return Mapper.Map<List<Status>>(domainStatus);
            }
        }

        public Status Find(int id)
        {
            using (uow = new UnitOfWork())
            {
                try
                {
                    return Mapper.Map<Status>(uow.StatusRepo.Find(id));
                }
                catch (NullReferenceException)
                {
                    return null;
                }
            }

        }
        public Status FindByTitle(string title)
        {
            using (uow = new UnitOfWork())
            {
                try
                {
                    return Mapper.Map<Status>(uow.StatusRepo.GetAll().Where(x => x.Title == title).FirstOrDefault());
                }
                catch (NullReferenceException)
                {
                    return null;
                }
            }

        }
    }

    public class PriorityService
    {
        private UnitOfWork uow;
        public List<Priority> PriorityList()
        {
            using (uow = new UnitOfWork())
            {
                var domainPriority = uow.PriorityRepo.GetAll().ToList();
                return Mapper.Map<List<Priority>>(domainPriority);
            }
        }
        public Priority Find(int id)
        {
            using (uow = new UnitOfWork())
            {
                try
                {
                    return Mapper.Map<Priority>(uow.PriorityRepo.Find(id));
                }
                catch (NullReferenceException)
                {
                    return null;
                }
            }

        }
        public Priority FindByTitle(string title)
        {
            using (uow = new UnitOfWork())
            {
                try
                {
                    return Mapper.Map<Priority>(uow.PriorityRepo.GetAll().Where(x => x.Title == title).FirstOrDefault());
                }
                catch (NullReferenceException)
                {
                    return null;
                }
            }

        }
    }
}