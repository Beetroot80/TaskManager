using Services.Interfaces;

using DomainEF.UnitOfWork;

namespace Services.Services
{
    public class ServiceCreator : IServiceCreator
    {
        public IUserService CreateUserService(string connection)
        {
            return new UserService(new IdentityUnitOfWork());
        }
    }
}
