using ServiceEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    interface IProjectService: IService<Project>
    {
        IEnumerable<string> GetTitles(string userId);
        IEnumerable<string> GetTitles();
        IEnumerable<Project> GetAll(string userId);
    }
}
