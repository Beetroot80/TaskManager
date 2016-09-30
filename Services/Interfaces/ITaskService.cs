using ServiceEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    interface ITaskService: IService<ServiceTask>
    {
        IEnumerable<ServiceTask> GetAll(string userId);
        IEnumerable<string> GetTitles();
        IEnumerable<Comment> GetComments(int taskId);
        IEnumerable<ServiceTask> GetAll(int projectId);
    }
}
