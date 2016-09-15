using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DomainCore;
using Repositories.Interfaces;
using UnitOfWork;
using DomainEF;
using System.Data.Entity;

namespace Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ITaskManagerContext context;

        public TaskRepository(IUnitOfWork uow)
        {
            this.context = uow.Context as ITaskManagerContext;
        }

        public List<DomainTask> TasksViaPriority(string status)
        {
            if (status != null)
                return context.Tasks.Where(x => x.Status.Title == status).ToList();
            return null;
        }

        public IQueryable<DomainTask> All()
        {
            return context.Tasks;
        }

        public IQueryable<DomainTask> AllIncluding(params Expression<Func<DomainTask, object>> [] includeProperties)
        {
            IQueryable<DomainTask> query = context.Tasks;
            foreach(var i in includeProperties)
            {
                query = query.Include(i);
            }
            return query;
        }

        public void Delete(int id)
        {
            var task = context.Tasks.Find(id);
            context.Tasks.Remove(task);
        }

        public DomainTask Find(int id)
        {
            return context.Tasks.Find(id);
        }

        public void InsertOrUpdate(DomainTask entity)
        {
            if(entity.Id == default(int)) //New
            {
                context.SetAdded(entity);
            }
            else //Excisting
            {
                context.SetModified(entity);
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        public void Dispose()
        {
            context.Dispose();
        }
        #endregion
    }
}
