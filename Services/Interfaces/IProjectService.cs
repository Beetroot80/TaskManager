using ServiceEntities;
using System.Collections.Generic;

namespace Services.Interfaces
{
    public interface IProjectService : IService<Project>
    {
        /// <summary>
        /// Returns all project with tasks included
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"
        IEnumerable<string> GetTitles(string userId);

        /// <summary>
        /// Returns all project titles
        /// </summary>
        IEnumerable<string> GetTitles();

        /// <summary>
        /// Returns all project that user own or included in
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IEnumerable<Project> GetAll(string userId);

        /// <summary>
        /// Find project by title. If not found null returned
        /// </summary>
        Project FindByTitle(string title);

        /// <summary>
        /// Return project with all related entities included
        /// </summary>
        /// <param name="projectId"></param>
        Project GetFullProject(int projectId);
    }
}
