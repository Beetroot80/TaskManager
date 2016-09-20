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
            ApplicationUser user = Uow.UserManager.Find(userDto.Email, userDto.Password);
            if (user != null)
                claims = Uow.UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
            return claims;
        }

        public OperationDetails Create(UserDTO userDto)
        {
            ApplicationUser user = Uow.UserManager.FindByEmail(userDto.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = userDto.Email, UserName = userDto.Email };
                Uow.UserManager.Create(user, userDto.Password);
                Uow.UserManager.AddToRole(user.Id, userDto.Role);
                ClientProfile clientProfile = new ClientProfile { Id = user.Id, Name = userDto.Name };
                Uow.ClientManager.Create(clientProfile);
                Uow.SaveChanges();
                return new OperationDetails(true, "Registraition is successful", "");
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
                    role = new ApplicationRole { Name = roleName };
                    Uow.RoleManager.Create(role);
                }
            }
            Create(adminDto);
        }
    }
}
