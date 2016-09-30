using System;
using System.Collections.Generic;
using System.Security.Claims;
using ServiceEntities;
using Services.Helpers;

namespace Services.Interfaces
{
    public interface IUserService : IService<ApplicationUser>
    {
        ClaimsIdentity Authenticate(ApplicationUser user);
        ApplicationUser GetUserById(string id);
    }
}
