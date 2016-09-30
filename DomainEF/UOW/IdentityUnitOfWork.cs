using System;
using Microsoft.AspNet.Identity.EntityFramework;

using DomainEntities;
using DomainEF.Identity;
using DomainEF.Interfaces;
using DomainEF.Repositories;

namespace DomainEF.UnitOfWork
{
    public class IdentityUnitOfWork : IIdentityUnitOfWork
    {
        private TaskManagerContext context;
        private ApplicationRoleManager roleManager;
        private ApplicationUserManager userManager;
        private IClientManager clientManager;

        public IdentityUnitOfWork()
        {
            Context = new TaskManagerContext();
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(Context));
            roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(Context));
            clientManager = new ClientManager(Context);
        }

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

        public TaskManagerContext Context
        {
            get
            {
                return context;
            }

            set
            {
                context = value;
            }
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        #region IDisposable 
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    UserManager.Dispose();
                    RoleManager.Dispose();
                    Context.Dispose();
                }

                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void SaveChanges(out bool? result)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges(out bool result)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
