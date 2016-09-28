using DomainEF.Identity;

namespace DomainEF.Interfaces
{
    public interface IIdentityUnitOfWork: IUnitOfWork
    {
        ApplicationUserManager UserManager { get; }
        IClientManager ClientManager { get; }
        ApplicationRoleManager RoleManager { get; }
    }
}
