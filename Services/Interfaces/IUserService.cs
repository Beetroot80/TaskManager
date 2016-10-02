using System.Security.Claims;
using ServiceEntities;

namespace Services.Interfaces
{
    public interface IUserService : IService<ApplicationUser>
    {
        /// <summary>
        /// Authenticate given user
        /// </summary>
        /// <param name="user"></param>
        ClaimsIdentity Authenticate(ApplicationUser user);
        ApplicationUser GetUserById(string id);
    }
}
