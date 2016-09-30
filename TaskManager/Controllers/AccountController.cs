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
using System.Linq;
using Services.Services;
using AutoMapper;

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
            //SetInitialData();

            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser { Email = model.Email, Password = model.Password };
                ClaimsIdentity claim = UserService.Authenticate(user);
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
            //SetInitialData();
            
            if (ModelState.IsValid)
            {
                OperationDetails opDetails = UserService.Create(Mapper.Map<ApplicationUser>(model));
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
            var roleService = new UserRoleService();
            TempData.Add("Roles", roleService.GetAllTitles());
            return PartialView();
        }

        [Authorize(Roles = "Administrator, Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUser(AddUserModel model) //TODO: how return the same window or display partial window
        {
            if (ModelState.IsValid)
            {
                var user = Mapper.Map<ApplicationUser>(model);
                var roles = new List<string>();
                if (model.Role != null)
                    roles.Add(model.Role);
                else
                    roles.Add("User");
                user.UserRoles = roles;
                OperationDetails opDetails = UserService.Create(user);
                if (opDetails.Succedeed)
                    return RedirectToAction("Index", "Home");
                else
                    ModelState.AddModelError(opDetails.Property, opDetails.Message);
            }
            var roleService = new UserRoleService();
            TempData ["Roles"] = roleService.GetAllTitles();
            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public ActionResult UsersList()
        {
            var users = UserService.GetAll();
            var userList = new List<EditUserModel>();
            foreach(var user in users)
            {
                userList.Add(AutoMapper.Mapper.Map<EditUserModel>(user));
            }
            var roleService = new UserRoleService();
            ViewBag.Roles = roleService.GetAllTitles();
            return PartialView(userList);
        }
        [Authorize(Roles = "Administrator, Manager")]
        public ActionResult UserEmails()
        {
            var users = UserService.GetAll().Select(x => x.Email).ToList();
            return Json(users, JsonRequestBehavior.AllowGet);
        }
    }
}