using System;
using DomainEntities;
using Microsoft.AspNet.Identity.EntityFramework;

using DomainEF.Repositories;
using DomainEF.Identity;
using DomainEF.Interfaces;


namespace DomainEF.UnitOfWork
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private TaskManagerContext context;

        private GenericRepository<Comment> commentRepo;
        private GenericRepository<DomainTask> domainTaskRepo;
        private GenericRepository<Priority> priorityRepo;
        private GenericRepository<Project> projectRepo;
        private GenericRepository<Status> statusRepo;

        private ApplicationRoleManager roleManager;
        private ApplicationUserManager userManager;
        private IClientManager clientManager;

        public TaskManagerContext Context
        {
            get
            {
                if (context == null)
                {
                    context = new TaskManagerContext();
                }
                return context;
            }
        }

        public IClientManager ClientManager
        {
            get
            {
                if (clientManager == null)
                {
                    clientManager = new ClientManager(context);
                }
                return clientManager;
            }
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                if (roleManager == null)
                {
                    roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(context));
                }
                return roleManager;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                if (userManager == null)
                {
                    userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
                }
                return userManager;
            }
        }

        public GenericRepository<Status> StatusRepo
        {
            get
            {
                if (statusRepo == null)
                {
                    statusRepo = new GenericRepository<Status>(context);
                }
                return statusRepo;
            }
        }

        public GenericRepository<Project> ProjectRepo
        {
            get
            {
                if (projectRepo == null)
                {
                    projectRepo = new GenericRepository<Project>(context);
                }
                return projectRepo;
            }
        }

        public GenericRepository<Priority> PriorityRepo
        {
            get
            {
                if (priorityRepo == null)
                {
                    priorityRepo = new GenericRepository<Priority>(context);
                }
                return priorityRepo;
            }
        }

        public GenericRepository<DomainTask> DomainTaskRepo
        {
            get
            {
                if (domainTaskRepo == null)
                {
                    domainTaskRepo = new GenericRepository<DomainTask>(context);
                }
                return domainTaskRepo;
            }
        }

        public GenericRepository<Comment> CommentRepo
        {
            get
            {
                if (commentRepo == null)
                {
                    commentRepo = new GenericRepository<Comment>(context);
                }
                return commentRepo;
            }
        }

        public UnitOfWork()
        {
            context = new TaskManagerContext();
        }
        public UnitOfWork(string connectionName) //TODO: implement
        {
            context = new TaskManagerContext();
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public void SaveChanges(out bool result)
        {
            int saved = context.SaveChanges();
            result = saved > 0 ? true : false;
        }

        #region IDisposable

        private void SafeDispose<T>(T o) where T : IDisposable
        {
            if (o != null)
                o.Dispose();
        }

        private bool disposedValue = false;
        void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    SafeDispose(userManager);
                    SafeDispose(clientManager);
                    SafeDispose(roleManager);
                    SafeDispose(commentRepo);
                    SafeDispose(domainTaskRepo);
                    SafeDispose(priorityRepo);
                    SafeDispose(projectRepo);
                    SafeDispose(statusRepo);
                    SafeDispose(context);
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
