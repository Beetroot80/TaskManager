using System.Collections.Generic;
using System.Linq;
using AutoMapper;

using ServiceEntities;

using DomainEF.UnitOfWork;

namespace Services.Services
{
    public class ProjectService
    {
        private UnitOfWork uow;

        public List<Project> GetAllProjects()
        {
            IEnumerable<DomainEntities.Project> dProjects = new List<DomainEntities.Project>();
            using (uow = new UnitOfWork())
            {
                dProjects = uow.ProjectRepo.GetAll().ToList();
            }
            List<Project> projects = new List<Project>();
            foreach (var entity in dProjects)
            {
                projects.Add(Mapper.Map<Project>(entity));
            }
            return projects;
        }

        public List<Project> GetAllProjectsWithCounts(string userId)
        {
            IEnumerable<DomainEntities.Project> dProjects = new List<DomainEntities.Project>();
            using (uow = new UnitOfWork())
            { //TODO: dispose connection before foreach, notice - gonna be exception if uow is disposed before mapping
                dProjects = uow.ProjectRepo.GetAllIncluding(null, null, x => x.Clients, y => y.Tasks)
                    .Where(x => x.CreatedById == userId || x.Clients.Where(y => y.Id == userId).Any()).ToList();//TODO: have to take counts without Including
                //TODO: recheck expression!
                List<Project> projects = new List<Project>();
                foreach (var entity in dProjects)
                {
                    projects.Add(Mapper.Map<Project>(entity));
                }
                return projects;
            }
        }

        public List<Project> GetAllProjectsWithTasks(string userId)//TODO: meaningful names!
        {
            IEnumerable<DomainEntities.Project> dProjects = new List<DomainEntities.Project>();
            using (uow = new UnitOfWork())
            {
                    dProjects = uow.ProjectRepo.GetAllIncluding(includeProperties: x => x.Tasks).ToList(); 
            }
            List<Project> projects = new List<Project>();
            foreach (var entity in dProjects)
            {
                projects.Add(Mapper.Map<Project>(entity));
            }
            return projects;
        }
        public List<Project> GetUserProjects(string userId)
        {
            IEnumerable<DomainEntities.Project> dProjects = new List<DomainEntities.Project>();
            using (uow = new UnitOfWork())
            {
                dProjects = uow.ProjectRepo.GetAll().Where(x => x.CreatedById == userId).ToList();
            }
            List<Project> projects = new List<Project>();
            foreach (var entity in dProjects)
            {
                projects.Add(Mapper.Map<Project>(entity));
            }
            return projects;
        }

        public List<Project> GetFullTasks() //TODO: delete
        {
            IEnumerable<DomainEntities.Project> dProjects = new List<DomainEntities.Project>();
            using (uow = new UnitOfWork())
            {
                dProjects = uow.ProjectRepo.GetAllIncluding(null, null, x => x.Clients, y => y.Tasks).ToList();
                foreach(var project in dProjects)
                {
                    foreach(var client in project.Clients)
                    {
                        client.ClientProfile = (DomainEntities.ClientProfile)uow.UserManager.Users.Select(c => c.ClientProfile).Where(p => p.Id == client.ClientProfileId);
                    }
                }
            }
            List<Project> projects = new List<Project>();

            foreach (var entity in dProjects)
            {
                projects.Add(Mapper.Map<Project>(entity));
            }
            return projects;
        }

        public void Addproject(Project project) //TODO: working if database excists and created by id excists
            //TODO: add some succed or failed view!
        {
            using (uow = new UnitOfWork())
            {
                uow.ProjectRepo.Insert(Mapper.Map<DomainEntities.Project>(project));
                uow.SaveChanges();
            }
        }
    }
}
