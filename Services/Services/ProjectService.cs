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
        //TODO: pass connection string in ctor

        public List<Project> GetAllProjects()
        {
            IEnumerable<DomainCore.Project> dProjects = new List<DomainCore.Project>();
            using (var uow = new UnitOfWork<TaskManagerContext>())
            {
                using (var repo = new ProjectRepository(uow))
                {
                    dProjects = repo.All();
                }
            }
            List<Project> projects = new List<Project>();
            foreach(var entity in dProjects)
            {
                projects.Add(Mapper.Map<Project>(entity));
            }
            return projects;
        }

        public List<Project> GetAllProjectsWithTasks()
        {
            IEnumerable<DomainCore.Project> dProjects = new List<DomainCore.Project>();
            using (var uow = new UnitOfWork<TaskManagerContext>())
            {
                using (var repo = new ProjectRepository(uow))
                {
                    dProjects = repo.GetAllWithTasks();
                }
            }
            List<Project> projects = new List<Project>();
            foreach (var entity in dProjects)
            {
                projects.Add(Mapper.Map<Project>(entity));
            }
            return projects;
        }

        public List<Project> GetFullTasks()
        {
            IEnumerable<DomainCore.Project> dProjects = new List<DomainCore.Project>();
            using (var uow = new UnitOfWork<TaskManagerContext>())
            {
                using (var repo = new ProjectRepository(uow))
                {
                    dProjects = repo.GetAllProjectsWithfullInfo();
                }
            }
            List<Project> projects = new List<Project>();

            //var serviceCreator = new ServiceCreator();
            //var userService = serviceCreator.CreateUserService("TaskManagerDB");

            //IEnumerable<ApplicationUser> users = userService.GetUsers();

            foreach (var entity in dProjects)
            {
                projects.Add(Mapper.Map<Project>(entity));
            }
            return projects;
        }
    }
}
