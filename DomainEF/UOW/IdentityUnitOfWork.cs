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
    public class IdentityUnitOfWork: IIdentityUnitOfWork
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
                throw new NotImplementedException();
            }
        }

        public IContext Context
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
