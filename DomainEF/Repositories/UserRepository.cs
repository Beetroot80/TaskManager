using DomainCore;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using DomainEF.Interfaces;
using UnitOfWork;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace DomainEF.Repositories
{
    public class ApplicationUserRepository : IEntityRepository<ApplicationUser>
    {
        private readonly ITaskManagerContext context; //TODO: Maybe change to IdentityDbContext

        public ApplicationUserRepository(IUnitOfWork uow)
        {
            this.context = new TaskManagerContext();
        }
        public ApplicationUser GetById(string id)
        {
            var identityContext = context as IdentityDbContext<ApplicationUser>;
            return identityContext.Users.Find(id);
        }

        public IEnumerable<string> GetAllRoles()
        {
            var identityContext = context as IdentityDbContext<ApplicationUser>;
            return identityContext.Roles.Select(x => x.Name).Distinct().ToList();
        }
        public IEnumerable<ApplicationUser> GetAllUsersWithRolesAndProfiles()
        {
            var identityContext = context as IdentityDbContext<ApplicationUser>;
            var users = identityContext.Users.Include(x => x.Roles).Include(x => x.ClientProfile).ToList();
            return users;
        }
        public IEnumerable<ApplicationUser> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ApplicationUser> AllIncluding(params Expression<Func<ApplicationUser, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public ApplicationUser Find(int id)
        {
            throw new NotImplementedException();
        }

        public void InsertOrUpdate(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }
    }
}
