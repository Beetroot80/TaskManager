using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainEF.Interfaces;
using DomainEF.UOW;
using DomainEF.Repositories;
using UnitOfWork;
using DomainEF;
using ServiceEntities;
using AutoMapper;
using ServiceMapper;

namespace Services.Services
{
    public class ProjectService
    {
        private UnitOfWork<TaskManagerContext> uow;

        public List<Project> GetAllProjects()
        {
            IEnumerable<DomainCore.Project> dProjects = new List<DomainCore.Project>();
            using (uow = new UnitOfWork<TaskManagerContext>())
            {
                dProjects = uow.ProjectRepo.All().ToList();
            }
            List<Project> projects = new List<Project>();
            foreach (var entity in dProjects)
            {
                projects.Add(Mapper.Map<Project>(entity));
            }
            return projects;
        }

        public List<Project> GetAllProjectsWithCounts()
        {
            IEnumerable<DomainCore.Project> dProjects = new List<DomainCore.Project>();
            using (uow = new UnitOfWork<TaskManagerContext>())
            { //TODO: dispose connection before foreach, notice - gonna be exception if uow is disposed before mapping
                dProjects = uow.ProjectRepo.AllIncluding(null, null, x => x.Clients, y => y.Tasks).ToList();//TODO: have to take counts without Including
                List<Project> projects = new List<Project>();
                foreach (var entity in dProjects)
                {
                    projects.Add(Mapper.Map<Project>(entity));
                }
                return projects;
            }
        }

        public List<Project> GetAllProjectsWithTasks()
        {
            IEnumerable<DomainCore.Project> dProjects = new List<DomainCore.Project>();
            using (uow = new UnitOfWork<TaskManagerContext>())
            {
                using (var repo = new ProjectRepository(uow))
                {
                    dProjects = uow.ProjectRepo.AllIncluding(includeProperties: x => x.Tasks).ToList();
                }
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
            IEnumerable<DomainCore.Project> dProjects = new List<DomainCore.Project>();
            using (uow = new UnitOfWork<TaskManagerContext>())
            {
                dProjects = uow.ProjectRepo.AllIncluding(null, null, x => x.Clients, y => y.Tasks).ToList();
                foreach(var project in dProjects)
                {
                    foreach(var client in project.Clients)
                    {
                        client.ClientProfile = (DomainCore.ClientProfile)uow.UserManager.Users.Select(c => c.ClientProfile).Where(p => p.Id == client.ClientProfileId);
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
    }
}
