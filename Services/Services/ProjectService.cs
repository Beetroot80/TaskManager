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

        public List<Project> GetAllProjectsWithCounts(string userId)
        {
            IEnumerable<DomainCore.Project> dProjects = new List<DomainCore.Project>();
            using (uow = new UnitOfWork<TaskManagerContext>())
            { //TODO: dispose connection before foreach, notice - gonna be exception if uow is disposed before mapping
                dProjects = uow.ProjectRepo.AllIncluding(null, null, x => x.Clients, y => y.Tasks)
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
            IEnumerable<DomainCore.Project> dProjects = new List<DomainCore.Project>();
            using (uow = new UnitOfWork<TaskManagerContext>())
            {
                    dProjects = uow.ProjectRepo.AllIncluding(includeProperties: x => x.Tasks).ToList(); 
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
            IEnumerable<DomainCore.Project> dProjects = new List<DomainCore.Project>();
            using (uow = new UnitOfWork<TaskManagerContext>())
            {
                dProjects = uow.ProjectRepo.All().Where(x => x.CreatedById == userId).ToList();
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

        public void Addproject(Project project) //TODO: working if database excists and created by id excists
            //TODO: add some succed or failed view!
        {
            using (uow = new UnitOfWork<TaskManagerContext>())
            {
                uow.ProjectRepo.Insert(Mapper.Map<DomainCore.Project>(project));
                uow.SaveChanges();
            }
        }
    }
}
