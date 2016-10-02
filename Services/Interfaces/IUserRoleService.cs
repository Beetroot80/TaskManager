using System.Collections.Generic;
using ServiceEntities;

namespace Services.Interfaces
{
    public interface IUserRoleService:IService<ApplicationRole>
    {
        /// <summary>
        /// Returns all roles names
        /// </summary>
        IEnumerable<string> GetAllTitles();
    }
}
