using System.Linq;
using System;
using System.Collections.Generic;
using ServiceEntities;
using AutoMapper;

using DomainEF.UnitOfWork;
using Services.Interfaces;
using Services.Helpers;

using domainStatus = DomainEntities.Status;
using domainPriority = DomainEntities.Priority;


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
                return new OperationDetails(result, result == true ? "Operation succed" : "Operation failed", "");
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

        public OperationDetails Update(Status item)
        {
            using (uow = new UnitOfWork())
            {
                try
                {
                    uow.StatusRepo.Update(Mapper.Map<domainStatus>(item));
                    uow.SaveChanges();
                    return new OperationDetails(true, "Updated", "Status");
                }
                catch (AutoMapperMappingException ex)
                {
                    return new OperationDetails(false, ex.Message, "Status");
                }
                catch (Exception ex)
                {
                    return new OperationDetails(false, ex.Message, "Status");
                }
            }
        }

        public OperationDetails Delete(Status item)
        {
            using (uow = new UnitOfWork())
            {
                try
                {
                    uow.StatusRepo.Delete(Mapper.Map<domainStatus>(item));
                    uow.SaveChanges();
                    return new OperationDetails(true, "Deleted", "Status");
                }
                catch (AutoMapperMappingException ex)
                {
                    return new OperationDetails(false, ex.Message, "Status");
                }
                catch (Exception ex)
                {
                    return new OperationDetails(false, ex.Message, "Status");
                }
            }
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
                return new OperationDetails(result, result == true ? "Operation succed" : "Operation failed", "");
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

        public OperationDetails Update(Priority item)
        {
            using (uow = new UnitOfWork())
            {
                try
                {
                    uow.PriorityRepo.Update(Mapper.Map<domainPriority>(item));
                    uow.SaveChanges();
                    return new OperationDetails(true, "Updated", "Priority");
                }
                catch (AutoMapperMappingException ex)
                {
                    return new OperationDetails(false, ex.Message, "Priority");
                }
                catch (Exception ex)
                {
                    return new OperationDetails(false, ex.Message, "Priority");
                }
            }
        }

        public OperationDetails Delete(Priority item)
        {
            using (uow = new UnitOfWork())
            {
                try
                {
                    uow.PriorityRepo.Delete(Mapper.Map<domainPriority>(item));
                    uow.SaveChanges();
                    return new OperationDetails(true, "Deleted", "Priority");
                }
                catch (AutoMapperMappingException ex)
                {
                    return new OperationDetails(false, ex.Message, "Priority");
                }
                catch (Exception ex)
                {
                    return new OperationDetails(false, ex.Message, "Priority");
                }
            }
        }
    }
}