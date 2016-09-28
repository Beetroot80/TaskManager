using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork;
using Repositories;
using DomainEF;
using ServiceEntities;
using AutoMapper;
using DomainCore;
using System.Collections;

namespace Services
{
    public class TaskService
    {
        private UnitOfWork<TaskManagerContext> uow;

        public IEnumerable<ServiceTask> GetAllTasks()
        {
            List<ServiceTask> serviseTasks;
            IEnumerable<DomainTask> tasks;
            using (uow = new UnitOfWork<TaskManagerContext>())
            {
                serviseTasks = new List<ServiceTask>();
                tasks = uow.DomainTaskRepo.AllIncluding(includeProperties: x => x.Client).ToList();
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
            using (uow = new UnitOfWork<TaskManagerContext>())
            {
                domainTasks = uow.DomainTaskRepo
                    .AllIncluding(null, null, signed => signed.Client, created => created.CreatedBy, status => status.Status, priority => priority.Priority, z => z.Project, m => m.CreatedBy)
                    .Where(x => x.ProjectId == projectId).ToList();
                foreach (var item in domainTasks)
                {
                    serviceTasks.Add(Mapper.Map<ServiceTask>(item));
                }
            }
            return serviceTasks;
        }
        public ServiceTask GetTaskById (int taskId)
        {
            using (uow = new UnitOfWork<TaskManagerContext>())
            {
                var domainTask = uow.DomainTaskRepo.AllIncluding(null, null, signed => signed.Client, created => created.CreatedBy, status => status.Status, priority => priority.Priority, z => z.Project, m => m.CreatedBy)
                    .Where(x => x.Id == taskId).FirstOrDefault();
                return Mapper.Map<ServiceTask>(domainTask);
            }
        }
        public List<ServiceEntities.Comment> GetComments(int taskId)
        {
            using (uow = new UnitOfWork<TaskManagerContext>())
            {
                var domainComments = uow.DomainTaskRepo.All().Where(x => x.Id == taskId).Select(x => x.Comments).FirstOrDefault();
                return Mapper.Map<List<ServiceEntities.Comment>>(domainComments);
            }
        }
    }
}
