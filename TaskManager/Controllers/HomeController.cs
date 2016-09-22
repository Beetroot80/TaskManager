using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskManager.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult AuthorizedAsAdmin()
        {
            throw new NotImplementedException();
        }
        [Authorize(Roles = "Manager")]
        public ActionResult AuthorizedAuthorizedAsManager()
        {
            throw new NotImplementedException();
        }
        [Authorize(Roles = "Manager")]
        public ActionResult AuthorizedAuthorizedAsUser()
        {
            throw new NotImplementedException();

        }
    }
}