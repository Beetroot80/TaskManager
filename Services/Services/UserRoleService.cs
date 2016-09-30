using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainEF.UnitOfWork;
using ServiceEntities;
using Services.Helpers;
using Services.Interfaces;

namespace Services.Services
{
    public class UserRoleService : IUserRoleService
    {
        private UnitOfWork uow;
        public OperationDetails Create(ApplicationRole item)
        {
            OperationDetails details;
            bool result;
            try
            {
                var role = Mapper.Map<DomainEntities.ApplicationRole>(item);
                using (uow = new UnitOfWork())
                {
                    uow.RoleManager.CreateAsync(role);
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

        public ApplicationRole Find(int id)
        {
            throw new NotImplementedException("Role mannager accepts string ids");
        }

        public IEnumerable<ApplicationRole> GetAll()
        {
            using (uow = new UnitOfWork())
            {
                return Mapper.Map<IEnumerable<ApplicationRole>>(uow.RoleManager.Roles.ToList());
            }
        }

        public IEnumerable<string> GetAllTitles()
        {
            using (uow = new UnitOfWork())
            {
                return uow.RoleManager.Roles
                .Select(x => x.Name)
                .Distinct()
                .ToList();
            }
        }

        public void Dispose()
        {
            if (uow != null)
                uow.Dispose();
        }

    }
}
