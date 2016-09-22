using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using DomainCore;
using DomainEF.Interfaces;
using DomainEF.UOW;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ServiceEntities;
using Services.Helpers;
using Services.Interfaces;
using UnitOfWork;
using appUser = DomainCore.ApplicationUser;
using appRole = DomainCore.ApplicationRole;
using AutoMapper;
using DomainEF.Repositories;

namespace Services.Services
{
    public class UserService : IUserService
    {
        IIdentityUnitOfWork Uow { get; set; }
        public UserService(IIdentityUnitOfWork uow)
        {
            Uow = uow;
        }

        public ClaimsIdentity Authenticate(UserDTO userDto)
        {
            ClaimsIdentity claims = null;
            appUser user = Uow.UserManager.Find(userDto.Email, userDto.Password);
            if (user != null)
                claims = Uow.UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
            return claims;
        }

        public OperationDetails Create(UserDTO userDto)
        {
            appUser user = Uow.UserManager.FindByEmail(userDto.Email);
            if (user == null)
            {
                user = new appUser { Email = userDto.Email, UserName = userDto.Email };
                Uow.UserManager.Create(user, userDto.Password);
                Uow.UserManager.AddToRole(user.Id, userDto.Role);
                DomainCore.ClientProfile clientProfile = new DomainCore.ClientProfile
                {
                    Id = user.Id,
                    Name = userDto.Name,
                    BirthDate = userDto.BirthDate,
                    PhoneNumber = userDto.PhoneNumber,
                    Surname = userDto.Surname
                };
                Uow.ClientManager.Create(clientProfile);
                Uow.SaveChanges();
                return new OperationDetails(true, "Registration is successful", "");
            }
            else
            {
                return new OperationDetails(false, "Error", "Email is already used");
            }
        }

        public void Dispose()
        {
            Uow.Dispose();
        }

        public void SetInitialDate(UserDTO adminDto, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = Uow.RoleManager.FindByName(roleName);
                if (role == null)
                {
                    role = new appRole { Name = roleName };
                    Uow.RoleManager.Create(role);
                }
            }
            Create(adminDto);
        }

        public IEnumerable<ServiceEntities.ApplicationUser> GetUsers() //TODO: delete
        {
            var domainUsers = Uow.UserManager.Users.ToList();
            List<ServiceEntities.ApplicationUser> users = new List<ServiceEntities.ApplicationUser>();
            foreach (var user in domainUsers)
            {
                users.Add(Mapper.Map<ServiceEntities.ApplicationUser>(user));
            }
            return users;
        }

        public ServiceEntities.ApplicationUser GetUserById(string id)
        {
            using (var repo = new ApplicationUserRepository(Uow))
            {
                return Mapper.Map<ServiceEntities.ApplicationUser>(repo.GetById(id));
            }
        }
    }
}
