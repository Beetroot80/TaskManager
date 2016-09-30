using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using ServiceEntities;
using AutoMapper;

using Services.Helpers;
using Services.Interfaces;

using System.Data.Entity;
using DomainEF.UnitOfWork;
using DomainEF.Interfaces;

using appUser = DomainEntities.ApplicationUser;
using serviceUser = ServiceEntities.ApplicationUser;
using System;

namespace Services.Services
{
    public class UserService : IUserService
    {
        private UnitOfWork uow;

        public UserService(IIdentityUnitOfWork unitOfWork) { }
        public UserService() { }

        public ClaimsIdentity Authenticate(serviceUser user)
        {
            ClaimsIdentity claims = null;
            using (uow = new UnitOfWork())
            {
                appUser domainUser = uow.UserManager.Find(user.Email, user.Password);
                if (domainUser != null)
                    claims = uow.UserManager.CreateIdentity(domainUser, DefaultAuthenticationTypes.ApplicationCookie);
                return claims;
            }
        }

        public OperationDetails Create(serviceUser item)
        {
            bool result;
            using (uow = new UnitOfWork())
            {
                appUser domainUser = uow.UserManager.FindByEmail(item.Email);
                if (domainUser == null)
                {
                    try
                    {
                        domainUser = new appUser() { Email = item.Email, UserName = item.UserName };
                        uow.UserManager.Create(domainUser, item.Password);
                        uow.SaveChanges();
                        uow.UserManager.AddToRole(domainUser.Id, item.UserRoles.First());
                        DomainEntities.ClientProfile clientProfile;
                        if (item.ClientProfile == null)
                        {
                            clientProfile = new DomainEntities.ClientProfile
                            {
                                Id = domainUser.Id,
                                Name = item.Email.Split('\u0040').ToString()
                            };
                        }
                        else
                            clientProfile = Mapper.Map<DomainEntities.ClientProfile>(item.ClientProfile);
                        uow.ClientManager.Create(clientProfile);
                        uow.SaveChanges(out result);
                        return new OperationDetails(true, "Registration is successful", "");
                    }
                    catch (AutoMapperMappingException ex)
                    {
                        return new OperationDetails(false, ex.Message, "Error ocured while saving");
                    }
                    catch (Exception ex)
                    {
                        return new OperationDetails(false, ex.Message, "Error ocured while saving");
                    }
                }
                else
                {
                    return new OperationDetails(false, "Error", "Email is already used");
                }
            }
        }

        public serviceUser Find(string id)
        {
            using (uow = new UnitOfWork())
            {
                try
                {
                    return Mapper.Map<serviceUser>(uow.UserManager.FindById(id));
                }
                catch (NullReferenceException)
                {
                    return null;
                }
            }

        }

        public serviceUser FindByEmail(string email)
        {
            using (uow = new UnitOfWork())
            {
                try
                {
                    return Mapper.Map<serviceUser>(uow.UserManager.FindByEmail(email));
                }
                catch (NullReferenceException)
                {
                    return null;
                }
            }
        }

        public IEnumerable<serviceUser> GetAll()
        {
            using (uow = new UnitOfWork())
            {
                var domainUsers = uow.UserManager.Users
                .Include(x => x.ClientProfile)
                .Include(x => x.Roles).ToList();
                foreach (var user in domainUsers)
                {
                    user.UserRoles = uow.UserManager.GetRoles(user.Id);
                }
                return Mapper.Map<IEnumerable<serviceUser>>(domainUsers);
            }
        }

        public serviceUser GetUserById(string id)
        {
            return Mapper.Map<serviceUser>(uow.UserManager.FindById(id));
        }

        public serviceUser Find(int id)
        {
            throw new NotImplementedException("User manager accepts string as ids");
        }

        public void Dispose()
        {
            if (uow != null)
                uow.Dispose();
        }

    }
}
