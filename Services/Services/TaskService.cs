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

        public IEnumerable<ServiceTask> GetSignedTasks(int projectId)
        {
            List<ServiceTask> serviceTasks = new List<ServiceTask>();
            IEnumerable<DomainTask> domainTasks;
            using (uow = new UnitOfWork<TaskManagerContext>())
            {
                domainTasks = uow.DomainTaskRepo
                    .AllIncluding(null, null, signed => signed.Client, created => created.CreatedBy, status => status.Status, priority => priority.Priority)
                    .Where(x => x.ProjectId == projectId).ToList();
                foreach (var item in domainTasks)
                {
                    serviceTasks.Add(Mapper.Map<ServiceTask>(item));
                }
            }
            return serviceTasks;
        }

    }
}
