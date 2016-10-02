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
using System;

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
            if (ModelState.IsValid)
            {
                var userService = new UserService();
                OperationDetails opDetails = userService.Create(Mapper.Map<ApplicationUser>(model));
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
        public ActionResult AddUser(AddUserModel model)
        {
            var roleService = new UserRoleService();
            IEnumerable<string> roleList = roleService.GetAllTitles();
            OperationDetails opDetails;
            if (ModelState.IsValid)
            {
                if (model.BirthDate != null)
                {
                    if (model.BirthDate >= DateTime.Now.Date)
                        ModelState.AddModelError("BirthDate", "Incorrect birth date");
                }

                var roleExcist = roleList.Contains(model.Role);
                if (!roleExcist)
                {
                    ModelState.AddModelError("Role", "Role does not exist");
                }
                var user = Mapper.Map<ApplicationUser>(model);
                var roles = new List<string>();

                user.UserRoles = model.Role;
                user.UserName = model.Name;
                opDetails = UserService.Create(user);

                if (opDetails.Succedeed) //TODO: Partial view Succeed
                    return RedirectToAction("Index", "Home");
                else //TODO: Partial view failed
                    ModelState.AddModelError(opDetails.Property, opDetails.Message);

            }
            TempData["Roles"] = roleList;
            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public ActionResult UsersList()
        {
            var users = UserService.GetAll();
            var roleService = new UserRoleService();
            var roleItems = roleService.GetAllTitles();
            var roleList = new SelectList(
               roleItems
              .Select(item => new SelectListItem
              {
                  Value = item.ToString(),
                  Text = item.ToString()
              }), "Value", "Text");
            var userList = new List<EditUserModel>();
            foreach (var user in users)
            {
                userList.Add(Mapper.Map<EditUserModel>(user));

            }
            foreach (var user in userList)
            {
                user.RoleList = roleList;
            }
            TempData["Roles"] = roleItems;
            return PartialView(userList);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult ChangeRole(string id, string newRole)
        {
            if (ModelState.IsValid)
            {
                var roleService = new UserRoleService();
                IEnumerable<string> roleList = roleService.GetAllTitles();
                if (roleList.Contains(newRole))
                {
                    var userService = new UserService();
                    var user = userService.Find(id);
                    user.UserRoles = newRole;
                    var opresults = userService.Update(user);
                    if (opresults.Succedeed == true)
                        return PartialView("Index", "Home"); //TODO: Succeed screen
                    else
                        return PartialView("Index", "Home"); //TODO: Error occurred
                }
                else
                    ModelState.AddModelError("NewRole", "Role does not exist");
            }
            return PartialView("Index", "Home");
        }

        [Authorize(Roles = "Administrator, Manager")]
        public ActionResult UserEmails()
        {
            var users = UserService.GetAll().Select(x => x.Email).ToList();
            return Json(users, JsonRequestBehavior.AllowGet);
        }
    }
}