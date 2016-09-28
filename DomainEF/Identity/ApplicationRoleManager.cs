using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

using DomainEntities;


namespace DomainEF.Identity
{
    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        public ApplicationRoleManager(RoleStore<ApplicationRole> store) 
            : base(store)
        {
        }
    }
}
