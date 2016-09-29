using System.Collections.Generic;
using System.Linq;
using AutoMapper;

using ServiceEntities;

using DomainEntities;
using DomainEF.UnitOfWork;
using Services.Services;

namespace Services
{
    public class TaskService
    {
        private UnitOfWork uow;

        public IEnumerable<ServiceTask> GetAllTasks()
        {
            List<ServiceTask> serviseTasks;
            IEnumerable<DomainTask> tasks;
            using (uow = new UnitOfWork())
            {
                serviseTasks = new List<ServiceTask>();
                tasks = uow.DomainTaskRepo.GetAllIncluding(includeProperties: x => x.Client).ToList();
            }
            foreach (var i in tasks)
            {
                serviseTasks.Add(Mapper.Map<ServiceTask>(i));
            }
            return serviseTasks;
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
        public List<ServiceEntities.Comment> GetComments(int taskId)
        {
            using (uow = new UnitOfWork())
            {
                var domainComments = uow.DomainTaskRepo.GetAll().Where(x => x.Id == taskId).Select(x => x.Comments).FirstOrDefault();
                return Mapper.Map<List<ServiceEntities.Comment>>(domainComments);
            }
        }
        public void AddTask(ServiceTask model)
        {
            using (uow = new UnitOfWork())
            {
                uow.DomainTaskRepo.Insert(Mapper.Map<DomainTask>(model));
                uow.SaveChanges();
            }
        }
    }
}
