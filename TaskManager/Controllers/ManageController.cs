using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ServiceEntities;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    public class ManageController : Controller
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
        // GET: Manage
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Save()
        {
            return View("Index");
        }

        [HttpPost]
        public ActionResult Save(ClientProfileModel model)
        {
            if(ModelState.IsValid)
            {
                ClientProfile profile = new ClientProfile { Name = model.Name, BirthDate = model.BirthDate, Surname = model.Surname };
                var user = User.Identity.Name;
                //ApplicationUser user = UserService.GetUserById(model.)
            }
            return View("Index");
        }
    }
}