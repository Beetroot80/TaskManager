using ServiceEntities;
using System.Collections.Generic;

namespace Services.Interfaces
{
    public interface ITaskService: IService<ServiceTask>
    {
        IEnumerable<ServiceTask> GetAll(string userId);
        IEnumerable<string> GetTitles();
        IEnumerable<Comment> GetComments(int taskId);
        IEnumerable<ServiceTask> GetAll(int projectId);
    }
}
