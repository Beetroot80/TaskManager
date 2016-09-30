using System.Collections.Generic;
using System.Linq;
using AutoMapper;

using ServiceEntities;
using Services.Interfaces;
using DomainEntities;
using DomainEF.UnitOfWork;
using Services.Helpers;
using System;

namespace Services
{
    public class TaskService : ITaskService
    {
        private UnitOfWork uow;

        public IEnumerable<ServiceTask> GetAll()
        {
            IEnumerable<DomainTask> tasks;
            using (uow = new UnitOfWork())
            {
                tasks = uow.DomainTaskRepo.GetAllIncluding(includeProperties: x => x.Client).ToList();
                return Mapper.Map<IEnumerable<ServiceTask>>(tasks);
            }
        }

        public IEnumerable<ServiceTask> GetAll(string userId)
        {
            IEnumerable<DomainTask> tasks;
            using (uow = new UnitOfWork())
            {
                tasks = uow.DomainTaskRepo.GetAll().Where(x => x.CreatedBy_Id == userId | x.AssignedTo == userId).ToList();
                return Mapper.Map<IEnumerable<ServiceTask>>(tasks);
            }
        }

        public IEnumerable<ServiceTask> GetAll(int projectId)
        {
            IEnumerable<DomainTask> tasks;
            using (uow = new UnitOfWork())
            {
                tasks = uow.DomainTaskRepo.GetAll().Where(x => x.ProjectId == projectId).ToList();
                return Mapper.Map<IEnumerable<ServiceTask>>(tasks);
            }
        }

        public IEnumerable<string> GetTitles()
        {
            IEnumerable<string> titles;
            using (uow = new UnitOfWork())
            {
                titles = uow.DomainTaskRepo.GetAll().Select(x => x.Title).ToList();
            }
            return titles;
        }
        public IEnumerable<ServiceEntities.Comment> GetComments(int taskId)
        {
            IEnumerable<DomainEntities.Comment> domainComments;
            using (uow = new UnitOfWork())
            {
                domainComments = uow.DomainTaskRepo.GetAll().Where(x => x.Id == taskId).Select(x => x.Comments).FirstOrDefault();
            }
            return Mapper.Map<IEnumerable<ServiceEntities.Comment>>(domainComments);
        }
        public ServiceTask Find(int taskId)
        {
            using (uow = new UnitOfWork())
            {
                return Mapper.Map<ServiceTask>(uow.DomainTaskRepo.Find(taskId));
            }
        }

        public IEnumerable<ServiceTask> GetSignedTasks(int projectId) //TODO: optimization!!!
        {
            List<ServiceTask> serviceTasks = new List<ServiceTask>();
            IEnumerable<DomainTask> domainTasks;
            using (uow = new UnitOfWork())
            {
                domainTasks = uow.DomainTaskRepo
                    .GetAllIncluding(null, null, signed => signed.Client, created => created.CreatedBy, status => status.Status, priority => priority.Priority, z => z.Project, m => m.CreatedBy)
                    .Where(x => x.ProjectId == projectId).ToList();
                foreach (var item in domainTasks)
                {
                    serviceTasks.Add(Mapper.Map<ServiceTask>(item));
                }
            }
            return serviceTasks;
        }
        public ServiceTask GetTaskById(int taskId)
        {
            using (uow = new UnitOfWork())
            {
                var domainTask = uow.DomainTaskRepo.GetAllIncluding(null, null, signed => signed.Client, created => created.CreatedBy, status => status.Status, priority => priority.Priority, z => z.Project, m => m.CreatedBy)
                    .Where(x => x.Id == taskId).FirstOrDefault();
                return Mapper.Map<ServiceTask>(domainTask);
            }
        }

        public OperationDetails Create(ServiceTask item)
        {
            OperationDetails details;
            bool result;
            try
            {
                var task = Mapper.Map<DomainTask>(item);
                using (uow = new UnitOfWork())
                {
                    uow.DomainTaskRepo.Insert(task);
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

    }
}
