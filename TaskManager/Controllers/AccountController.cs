﻿using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using TaskManager.Models;
using Services.Interfaces;
using Services.Services;
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

        [HttpGet]
        public ActionResult AddUser()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUser(AddUserModel model)
        {
            if(ModelState.IsValid)
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
                    return View("Index", "Home");
                else
                    ModelState.AddModelError(opDetails.Property, opDetails.Message);
            }

            return View("AuthorizedAsAdmin", "Home", model);
        }

        private void SetInitialData()
        {
            UserService.SetInitialDate(new UserDTO
            {
                Email = "admin@gmail.com",
                UserName = "admin@gmail.com",
                Password = "Admin1!",
                Name = "Admin",
                Role = "Administrator",
            }, new List<string> { "user", "admin" });
        }
    }
}