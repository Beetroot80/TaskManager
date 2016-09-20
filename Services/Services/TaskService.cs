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

namespace Services
{
    public class TaskService
    {
        public static IEnumerable<ServiceTask> GetAllTasks()
        {
            using (var uow = new UnitOfWork<TaskManagerContext>())
            {
                using (var repo = new TaskRepository(uow))
                {
                    List<ServiceTask> serviseTasks = new List<ServiceTask>();
                    var tasks = repo.AllIncluding(x => x.User);
                    foreach(var i in tasks)
                    {
                        serviseTasks.Add(Mapper.Map<ServiceTask>(i));
                    }
                    return serviseTasks;
                }
            }
        }
    
    }
}
