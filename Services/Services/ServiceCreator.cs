using Services.Interfaces;

using DomainEF.UnitOfWork;

namespace Services.Services
{
    public class ServiceCreator : IServiceCreator
    {
        //In plans create per one of each service for one OwinContext
        public IUserService CreateUserService(string connection)
        {
            return new UserService(new IdentityUnitOfWork());
        }
    }
}
