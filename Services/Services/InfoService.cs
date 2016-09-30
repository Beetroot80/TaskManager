using System.Linq;
using System;
using System.Collections.Generic;
using ServiceEntities;
using AutoMapper;

using DomainEF.UnitOfWork;
using Services.Interfaces;
using Services.Helpers;


namespace Services.Services
{
    public class StatusService : IStatusService
    {
        private UnitOfWork uow;

        public IEnumerable<Status> GetAll()
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

        public OperationDetails Create(Status item)
        {
            OperationDetails details;
            bool result;
            try
            {
                var status = Mapper.Map<DomainEntities.Status>(item);
                using (uow = new UnitOfWork())
                {
                    uow.StatusRepo.Insert(status);
                    uow.SaveChanges(out result);
                }
                return new OperationDetails(result, "", "");
            }
            catch (AutoMapperMappingException ex)
            {
                return details = new OperationDetails(false, ex.Message, "");
            }
            catch (Exception ex)
            {
                return details = new OperationDetails(false, ex.Message, "");
            }
        }

        public void Dispose()
        {
            if (uow != null)
                uow.Dispose();
        }
    }

    public class PriorityService : IPriorityService
    {
        private UnitOfWork uow;

        public IEnumerable<Priority> GetAll()
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

        public OperationDetails Create(Priority item)
        {
            OperationDetails details;
            bool result;
            try
            {
                var priority = Mapper.Map<DomainEntities.Priority>(item);
                using (uow = new UnitOfWork())
                {
                    uow.PriorityRepo.Insert(priority);
                    uow.SaveChanges(out result);
                }
                return new OperationDetails(result, "", "");
            }
            catch (AutoMapperMappingException ex)
            {
                return details = new OperationDetails(false, ex.Message, "");
            }
            catch (Exception ex)
            {
                return details = new OperationDetails(false, ex.Message, "");
            }
        }

        public void Dispose()
        {
            if (uow != null)
                uow.Dispose();
        }
    }
}