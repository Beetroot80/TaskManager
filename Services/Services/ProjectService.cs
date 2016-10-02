using System.Collections.Generic;
using System.Linq;
using AutoMapper;

using ServiceEntities;
using Services.Interfaces;
using DomainEF.UnitOfWork;
using System;
using Services.Helpers;

namespace Services.Services
{
    public class ProjectService : IProjectService
    {
        private UnitOfWork uow;

        public IEnumerable<Project> GetAll()
        {
            IEnumerable<DomainEntities.Project> domainProjects;
            using (uow = new UnitOfWork())
            {
                domainProjects = uow.ProjectRepo.GetAll().ToList();
                return Mapper.Map<IEnumerable<Project>>(domainProjects);
            }
        }

        public IEnumerable<Project> GetAll(string userId)
        {
            using (uow = new UnitOfWork())
            {
                var domainProjects = uow.ProjectRepo.GetAll().Where(x => x.CreatedById == userId).ToList();
                return Mapper.Map<IEnumerable<Project>>(domainProjects);
            }
        }

        public IEnumerable<string> GetTitles()
        {
            using (uow = new UnitOfWork())
            {
                return uow.ProjectRepo.GetAll().Select(x => x.Title).ToList();
            }
        }

        public IEnumerable<string> GetTitles(string userId) //TODO: do i need all created by or cleits of also?
        {
            using (uow = new UnitOfWork())
            {
                return uow.ProjectRepo.GetAll().Where(x => x.CreatedById == userId).Select(x => x.Title).ToList();
            }
        }

        public Project Find(int id)
        {
            using (uow = new UnitOfWork())
            {
                try
                {
                    return Mapper.Map<Project>(uow.ProjectRepo.Find(id));
                }
                catch (NullReferenceException)
                {
                    return null;
                }
            }
        }

        public Project FindByTitle(string title)
        {
            using (uow = new UnitOfWork())
            {
                try
                {
                    return Mapper.Map<Project>(uow.ProjectRepo.GetAll().Where(x => x.Title == title).FirstOrDefault());
                }
                catch (NullReferenceException)
                {
                    return null;
                }
            }
        }

        public OperationDetails Create(Project item)
        {
            OperationDetails details;
            bool result;
            try
            {
                var project = Mapper.Map<DomainEntities.Project>(item);
                using (uow = new UnitOfWork())
                {
                    uow.ProjectRepo.Insert(project);
                    uow.SaveChanges(out result);
                }
                return new OperationDetails(result, result == true ? "Operation succed" : "Operation failed", "");
            }
            catch (AutoMapperMappingException ex)
            {
                return details = new OperationDetails(false, ex.Message, "");
            }
            catch (Exception ex)
            {
                return details = new OperationDetails(false, ex.Message, "");
            }
        }

        public IEnumerable<Project> GetProjectsWithCounters(string userId)
        {
            using (uow = new UnitOfWork())
            {
                var domainProjects = uow.ProjectRepo
                    .GetAll()
                    .Where(x => x.CreatedById == userId || x.Clients.Where(y => y.Id == userId).Any())
                    .ToList();
                return Mapper.Map<IEnumerable<Project>>(domainProjects);
            }
        }

        public IEnumerable<Project> GetProjectsWithTaskIncluded(string userId)//TODO: do i need this
        {
            using (uow = new UnitOfWork())
            {
               var domainProjects = uow.ProjectRepo.GetAllIncluding(includeProperties: x => x.Tasks).ToList();
                return Mapper.Map<IEnumerable<Project>>(domainProjects);
            }
        }
        public Project GetProjectWithTaskByTitle(string projectTitle)
        {
            using (uow = new UnitOfWork())
            {
                var domainProjects = uow.ProjectRepo.GetAllIncluding(includeProperties: x => x.Tasks).Where(x => x.Title == projectTitle).FirstOrDefault();
                return Mapper.Map<Project>(domainProjects);
            }
        }

        public void Dispose()
        {
            if (uow != null)
                uow.Dispose();
        }

        public OperationDetails Update(Project item)
        {
            using (uow = new UnitOfWork())
            {
                try
                {
                    uow.ProjectRepo.Update(Mapper.Map<DomainEntities.Project>(item));
                    uow.SaveChanges();
                    return new OperationDetails(true, "Updated", "Project");
                }
                catch (AutoMapperMappingException ex)
                {
                    return new OperationDetails(false, ex.Message, "Project");
                }
                catch (Exception ex)
                {
                    return new OperationDetails(false, ex.Message, "Project");
                }
            }
        }

        public OperationDetails Delete(Project item)
        {
            using (uow = new UnitOfWork())
            {
                try
                {
                    var oldItem = new DomainEntities.Project();
                    oldItem.Id = item.Id;
                    uow.ProjectRepo.Delete(oldItem);
                    uow.SaveChanges();
                    return new OperationDetails(true, "Deleted", "Project");
                }
                catch (AutoMapperMappingException ex)
                {
                    return new OperationDetails(false, ex.Message, "Project");
                }
                catch (Exception ex)
                {
                    return new OperationDetails(false, ex.Message, "Project");
                }
            }
        }

        public OperationDetails Delete(int itemId)
        {
            using (uow = new UnitOfWork())
            {
                try
                {

                    uow.ProjectRepo.Delete(itemId);
                    uow.SaveChanges();
                    return new OperationDetails(true, "Deleted", "Project");
                }
                catch (AutoMapperMappingException ex)
                {
                    return new OperationDetails(false, ex.Message, "Project");
                }
                catch (Exception ex)
                {
                    return new OperationDetails(false, ex.Message, "Project");
                }
            }
        }
    }
}
