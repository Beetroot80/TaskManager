using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ServiceEntities;
using Services.Helpers;

namespace Services.Interfaces
{
    public interface IUserService : IDisposable
    {
        OperationDetails Create(UserDTO userDto);
        ClaimsIdentity Authenticate(UserDTO userDto);
        void SetInitialDate(UserDTO adminDto, List<string> roles);
        IEnumerable<ApplicationUser> GetUsers();
    }
}
