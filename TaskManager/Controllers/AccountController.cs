using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using TaskManager.Models;
using Services.Interfaces;
using ServiceEntities;
using Services.Helpers;
using System.Collections.Generic;

namespace TaskManager.Controllers
{

    public class AccountController : Controller
    {
        private IUserService UserService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            SetInitialData();

            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO { Email = model.Email, Password = model.Password };
                ClaimsIdentity claim = UserService.Authenticate(userDto);
                if (claim == null)
                {
                    ModelState.AddModelError("", "Incorrect login or password");
                }
                else
                {
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            SetInitialData();

            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO
                {
                    Email = model.Email,
                    Password = model.Password,
                    Name = model.Name,
                    Role = "user"
                };
                OperationDetails opDetails = UserService.Create(userDto);
                if (opDetails.Succedeed)
                    return View("SuccessRegister");
                else
                    ModelState.AddModelError(opDetails.Property, opDetails.Message);
            }
            return View(model);
        }

        [Authorize(Roles = "Administrator, Manager")]
        [HttpGet]
        public ActionResult AddUser()
        {
            TempData.Add("Roles", UserService.GetAllRoles());
            return PartialView();
        }

        [Authorize(Roles = "Administrator, Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUser(AddUserModel model) //TODO: how return the same window or display partial window
        {
            if (ModelState.IsValid)
            {
                UserDTO user = new UserDTO
                {
                    Email = model.Email,
                    Password = model.Password,
                    UserName = model.Email,
                    Name = model.Name,
                    Role = model.Role,
                    PhoneNumber = model.PhoneNumber,
                    Surname = model.Surname
                };
                OperationDetails opDetails = UserService.Create(user);
                if (opDetails.Succedeed)
                    return RedirectToAction("Index", "Home");
                else
                    ModelState.AddModelError(opDetails.Property, opDetails.Message);
            }
            TempData["Roles"] = UserService.GetAllRoles();
            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public ActionResult UsersList()
        {
            var users = UserService.GetUsers();
            var userList = new List<EditUserModel>();
            foreach(var user in users)
            {
                userList.Add(AutoMapper.Mapper.Map<EditUserModel>(user));
            }

            ViewBag.Roles = UserService.GetAllRoles();
            return PartialView(userList);
        }

        private void SetInitialData() //TODO: remove
        {
            UserService.SetInitialDate(new UserDTO
            {
                Email = "admin@gmail.com",
                UserName = "admin@gmail.com",
                Password = "Admin1!",
                Name = "Admin",
                Role = "Administrator",
            }, new List<string> { "User", "Manager" });
        }
    }
}