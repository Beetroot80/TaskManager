using Microsoft.AspNet.Identity;
using Services.Services;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    public class ManageController : Controller
    {
        [Authorize(Roles = "Administrator, Manager, User")]
        [HttpGet]
        public ActionResult Manage()
        {
            var manageModel = new ManageModel();
            if (User.IsInRole("Administrator") || User.IsInRole("Manager"))
            {
                manageModel.ProjectActions = new List<string>()
                {
                    "Add user to project",
                    "Delete project",
                    "Manage tasks"
                };
                manageModel.TaskAction = new List<string>()
                {
                    "Assign to user",
                    "Delete"
                };
            }
            else
            {
                manageModel.ProjectActions = new List<string>()
                {
                    "Delete project",
                    "Manage tasks"
                };
                manageModel.TaskAction = new List<string>()
                {
                    "Delete"
                };
            }
            var projectService = new ProjectService();
            TempData["ProjectTitles"] = projectService.GetUserProjects(User.Identity.GetUserId()).Select(x => x.Title).ToList();
            return PartialView("Manage", manageModel);
        }

        [Authorize(Roles = "Administrator, Manager, User")]
        [HttpPost]
        public ActionResult Manage(ManageModel model)
        {
            return PartialView("Manage");
        }
    }
}