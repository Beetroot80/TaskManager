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
        public UnitOfWork Uow
        {
            get
            {
                if (uow == null)
                    uow = new UnitOfWork();
                return uow;
            }
        }

        public UserService(IIdentityUnitOfWork unitOfWork)
        {
        }
        public UserService()
        {
        }

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

        public OperationDetails Update(serviceUser user)
        {
            using (uow = new UnitOfWork())
            {
                var oldUser = uow.UserManager.FindById(user.Id);
                oldUser.UserName = user.UserName;
                oldUser.UserRoles = new List<string>() { user.UserRoles };
                oldUser.Email = user.Email;
                oldUser.PasswordHash =
                    user.Password == null ?
                    oldUser.PasswordHash :
                    uow.UserManager.PasswordHasher.HashPassword(user.Password);
                var identityResult = uow.UserManager.Update(oldUser);
                return new OperationDetails(identityResult.Succeeded,
                    identityResult.Succeeded == true ? "Success" : "Error",
                    identityResult.Succeeded == true ? "User updated" : "Error occurred while updating"
                    );
            }

        }
        public OperationDetails Create(serviceUser item)
        {
            using (uow = new UnitOfWork())
            {
                try
                {
                    appUser user = uow.UserManager.FindByEmail(item.Email);
                    if (user == null)
                    {
                        user = new appUser
                        {
                            Email = item.Email,
                            UserName = item.UserName
                        };
                        uow.UserManager.Create(user, item.Password);

                        DomainEntities.ClientProfile clientProfile = new DomainEntities.ClientProfile
                        {
                            Id = user.Id,
                            ApplicationUser = user,
                            Name = item.UserName,
                            Surname = item.Surname,
                            BirthDate = item.BirthDate
                        };
                        user.ClientProfileId = clientProfile.Id;
                        uow.ClientManager.Create(clientProfile);
                        uow.UserManager.AddToRole(user.Id, item.UserRoles ?? "User");
                        return new OperationDetails(true, "Registration is successful", "");
                    }
                    else
                    {
                        return new OperationDetails(false, "Error", "Email is already used");
                    }
                }
                catch (AutoMapperMappingException ex)
                {
                    return new OperationDetails(false, ex.Message, "Error occurred while saving");
                }
                catch (Exception ex)
                {
                    return new OperationDetails(false, ex.Message, "Error occurred while saving");
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

        public OperationDetails Delete(serviceUser item)
        {
            using (uow = new UnitOfWork())
            {
                try
                {
                    uow.UserManager.Delete(Mapper.Map<appUser>(item));
                    return new OperationDetails(true, "Deleted", "User");
                }
                catch (AutoMapperMappingException ex)
                {
                    return new OperationDetails(false, ex.Message, "User");
                }
                catch (NullReferenceException ex)
                {
                    return new OperationDetails(false, ex.Message, "User");
                }
                catch (Exception ex)
                {
                    return new OperationDetails(false, ex.Message, "User");
                }
            }
        }
    }
}
