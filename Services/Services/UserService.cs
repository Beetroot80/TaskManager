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
using appRole = DomainEntities.ApplicationRole;
using serviceUser = ServiceEntities.ApplicationUser;
using System;

namespace Services.Services
{
    public class UserService : IUserService
    {
        //TODO: usings?
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
                DomainEntities.ClientProfile clientProfile = new DomainEntities.ClientProfile
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

        public ServiceEntities.ApplicationUser Find(string id)
        {
            using (uow = new UnitOfWork())
            {
                try
                {
                    return Mapper.Map<ServiceEntities.ApplicationUser>(uow.UserManager.FindById(id));
                }
                catch (NullReferenceException)
                {
                    return null;
                }
            }

        }
        public ServiceEntities.ApplicationUser FindByEmail(string email)
        {
            using (uow = new UnitOfWork())
            {
                try
                {
                    return Mapper.Map<ServiceEntities.ApplicationUser>(uow.UserManager.FindByEmail(email));
                }
                catch (NullReferenceException)
                {
                    return null;
                }
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

        public IEnumerable<serviceUser> GetUsers()
        {
            IEnumerable<appUser> domainUsers;
            domainUsers = Uow.UserManager.Users
                .Include(x => x.ClientProfile)
                .Include(x => x.Roles).ToList();

            foreach (var user in domainUsers) //TODO: refactor this
            {
                var roleIds = user.Roles.Select(x => x.RoleId);
                var rolenames = new List<string>();
                foreach (var id in roleIds)
                {
                    rolenames.Add(Uow.RoleManager.FindById(id).Name);
                }
                user.UserRoles = rolenames;

            }

            List<serviceUser> users = new List<serviceUser>();
            foreach (var user in domainUsers)
            {
                users.Add(Mapper.Map<serviceUser>(user));
            }
            return users;
        }

        public ServiceEntities.ApplicationUser GetUserById(string id)
        {
            return Mapper.Map<serviceUser>(Uow.UserManager.FindById(id));
        }

        public IEnumerable<string> GetAllRoles()//TODO: should be in a repo?
        {
            return Uow.RoleManager.Roles
            .Select(x => x.Name)
            .Distinct()
            .ToList();
        }
    }
}
