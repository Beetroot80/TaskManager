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

        public IEnumerable<string> GetTitles(string userId)
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
                catch(Exception)
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
                return new OperationDetails(result, result == true ? "Operation succeed" : "Operation failed", "");
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
                IEnumerable<DomainEntities.Project> domainProjects;

                domainProjects = uow.ProjectRepo.GetAllIncluding(null, null, x => x.Clients, x => x.Tasks).Where(x => x.CreatedById == userId).ToList();
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
        public Project GetFullProject(int projectId)
        {
            using (uow = new UnitOfWork())
            {
                var domainProjects = uow.ProjectRepo.GetAllIncluding(null, null, x => x.Tasks, x=> x.Clients, x=> x.CreatedBy).Where(x => x.Id == projectId).FirstOrDefault();
                return Mapper.Map<Project>(domainProjects);
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
                    var i = new DomainEntities.Project();
                    i.Clients = Mapper.Map<ICollection<DomainEntities.ApplicationUser>>(item.Clients);
                    i.Id = item.Id;
                    uow.ProjectRepo.Update(i);
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
