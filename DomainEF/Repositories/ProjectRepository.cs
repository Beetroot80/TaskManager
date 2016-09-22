using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DomainCore;
using DomainEF.Interfaces;
using UnitOfWork;
using DomainEF.UOW;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DomainEF.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ITaskManagerContext context;

        public ProjectRepository(IUnitOfWork uow)
        {
            this.context = uow.Context as ITaskManagerContext;
        }

        public IEnumerable<Project> GetAllProjectsWithfullInfo()
        {
            return context.Projects.Include(x => x.Tasks).Include(x => x.Clients).ToList();
        }
        public IEnumerable<Project> GetAllWithTasks()
        {
            return context.Projects.Include("Tasks").ToList();
        }
        public IEnumerable<Project> All()
        {
            return context.Projects.ToList();
        }

        public IEnumerable<Project> AllIncluding(params Expression<Func<Project, object>> [] includeProperties)
        {
            IQueryable<Project> query = context.Projects;
            foreach (var i in includeProperties)
            {
                query = query.Include(i);
            }
            return query.ToList();
        }

        public void Delete(int id)
        {
            var project = context.Projects.Find(id);
            context.Projects.Remove(project);
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public Project Find(int id)
        {
            return context.Projects.Find(id);
        }

        public void InsertOrUpdate(Project entity)
        {
            if (entity.Id == default(int)) //New
            {
                context.SetAdded(entity);
            }
            else //Excisting
            {
                context.SetModified(entity);
            }
        }
    }
}
