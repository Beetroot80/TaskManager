using System.Collections.Generic;
using ServiceEntities;

namespace Services.Interfaces
{
    public interface IUserRoleService:IService<ApplicationRole>
    {
        IEnumerable<string> GetAllTitles();
    }
}
