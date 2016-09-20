using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainEF.Identity;
using UnitOfWork;

namespace DomainEF.Interfaces
{
    public interface IIdentityUnitOfWork: IUnitOfWork
    {
        ApplicationUserManager UserManager { get; }
        IClientManager ClientManager { get; }
        ApplicationRoleManager RoleManager { get; }
    }
}
