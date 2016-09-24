using System;
using System.Data.Entity;
using DomainEF;
using DomainCore;
using DomainEF.Repositories;
using DomainEF.Identity;
using DomainEF.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;

namespace UnitOfWork
{
    public sealed class UnitOfWork<TContext>: IUnitOfWork
        where TContext : IContext, new() //TODO: Do I need this?
    {
        private TaskManagerContext context;

        private GenericRepository<ApplicationUser> applicationUserRepo; //TODO: do i need these 2?
        private GenericRepository<ApplicationRole> applicationRoleRepo;
        private GenericRepository<ClientProfile> clientProfileRepo;

        private GenericRepository<Comment> commentRepo;
        private GenericRepository<DomainTask> domainTaskRepo;
        private GenericRepository<Priority> priorityRepo;
        private GenericRepository<Project> projectRepo;
        private GenericRepository<Status> statusRepo;

        private ApplicationRoleManager roleManager;
        private ApplicationUserManager userManager;
        private IClientManager clientManager;


        public IClientManager ClientManager
        {
            get
            {
                return clientManager;
            }
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return roleManager;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
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
                if(projectRepo == null)
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
                if(priorityRepo == null)
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
                if(domainTaskRepo == null)
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
                if(commentRepo == null)
                {
                    commentRepo = new GenericRepository<Comment>(context);
                }
                return commentRepo;
            }
        }

        public UnitOfWork() //Pass connection string? 
        {
            context = new TaskManagerContext();
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(context));
            clientManager = new ClientManager(context);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        #region IDisposable

        private bool disposedValue = false;
        void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    UserManager.Dispose();
                    RoleManager.Dispose();
                    context.Dispose();
                    context = null;
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
