using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainCore;
using DomainEF.Identity;
using DomainEF.Interfaces;
using DomainEF.Repositories;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DomainEF.UOW
{
    public class IdentityUnitOfWork : IIdentityUnitOfWork
    {
        private TaskManagerContext context; //TODO: can change to ITaskManagerContext or TaskManagerContext
        private ApplicationRoleManager roleManager;
        private ApplicationUserManager userManager;
        private IClientManager clientManager;

        public IdentityUnitOfWork(string connection)
        {
            context = new TaskManagerContext();
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(context));
            clientManager = new ClientManager(context);
        }

        public IClientManager ClientManager
        {
            get
            {
                return clientManager;
            }
        }

        public IContext Context
        {
            get
            {
                return context;
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


        public void SaveChanges()
        {
            context.SaveChanges();
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
        #endregion
    }
}
