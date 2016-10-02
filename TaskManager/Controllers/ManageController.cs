using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

using Services;
using Services.Helpers;
using Services.Services;
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
                    //"Add user to project",
                    "Delete project",
                    "Manage tasks"
                };
                manageModel.TaskAction = new List<string>()
                {
                    //"Assign to user",
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
            TempData["ProjectTitles"] = projectService.GetAll(User.Identity.GetUserId()).Select(x => x.Title).ToList();
            return PartialView("Manage", manageModel);
        }

        //This code is just awful, have to totally refactor this!
        [Authorize(Roles = "Administrator, Manager, User")]
        [HttpPost]
        public void Manage(string projectTitle, string taskTitle, string projectAction, string taskAction, string assignTo)
        {
            #region settings
            var projectService = new ProjectService();
            var adminsTaskAction = new List<string>()
                {
                    "Assign to user",
                    "Delete"
                };
            var userTaskAction = new List<string>()
                {
                    "Delete"
                };
            var adminsProjectAction = new List<string>()
                {
                    "Add user to project",
                    "Delete project",
                    "Manage tasks"
                };
            var userProjectAction = new List<string>()
                {
                    "Delete project",
                    "Manage tasks"
                };

            var projectTitles = projectService.GetAll(User.Identity.GetUserId()).Select(x => x.Title).ToList();
            bool isInAdminrole = User.IsInRole("Administrator") || User.IsInRole("Manager");
            #endregion

            if (projectTitle == null || !projectTitles.Contains(projectTitle))
            {
                ModelState.AddModelError("ProjecTtitle", "Title does not exist");
            }
            else
            {
                var projectId = projectService.FindByTitle(projectTitle).Id;
                if (isInAdminrole && adminsProjectAction.Contains(projectAction))
                {
                    switch (projectAction)
                    {
                        case "Delete project":
                            DeleteProject(projectId);
                            break;
                        case "Add user to project":
                            AddUserToProject(projectId, assignTo);
                            break;
                        case "Manage tasks":
                            if (adminsTaskAction.Contains(taskAction))
                            {
                                switch (taskAction)
                                {
                                    case "Assign to user":
                                        AssignToTask(projectId, taskTitle, assignTo);
                                        break;
                                    case "Delete":
                                        DeleteTask(projectId, taskTitle);
                                        break;
                                }
                            }
                            else
                                ModelState.AddModelError("TaskActions", "Invalid task action");
                            break;
                    }
                }
                else if (!isInAdminrole && userProjectAction.Contains(projectAction))
                {
                    switch (projectAction)
                    {
                        case "Delete":
                            DeleteProject(projectId);
                            break;
                        case "Manage tasks":
                            if (userTaskAction.Contains(taskAction))
                            {
                                switch (taskAction)
                                {
                                    case "Delete":
                                        DeleteTask(projectId, taskTitle);
                                        break;
                                }
                            }
                            else
                                ModelState.AddModelError("TaskActions", "Invalid task action");
                            break;
                    }
                }
                else
                    ModelState.AddModelError("ProjectActions", "Title does not exist");
            }
        }

        private ActionResult DeleteProject(int projectId)
        {
            var projectService = new ProjectService();
            var opDetails = projectService.Delete(projectId);
            return Result(opDetails);
        }

        private ActionResult DeleteTask(int projectId, string taskTitle)
        {
            var taskService = new TaskService();
            var task = taskService.GetAll(projectId).Where(x => x.Title == taskTitle).FirstOrDefault();
            var opDetails = taskService.Delete(task);
            return Result(opDetails);
        }

        private ActionResult AddUserToProject(int projectId, string assignTo)
        {
            var projectService = new ProjectService();
            var userService = new UserService();
            var project = projectService.GetFullProject(projectId);

            var user = userService.FindByEmail(assignTo);
            OperationDetails opDetails;
            if (user != null && !project.Clients.Contains(user))
            {
                project.Clients.Add(user);
                opDetails = projectService.Update(project);
                return Result(opDetails);
            }
            return Json(new { success = false, responseText = user == null ? "User was not found" : "User already signet for project" }, JsonRequestBehavior.AllowGet);
        }

        private ActionResult AssignToTask(int projectId, string taskTitle, string assignTo)
        {
            var userService = new UserService();
            var taskService = new TaskService();
            var projectService = new ProjectService();
            var project = projectService.Find(projectId);
            var task = taskService.GetAll(projectId).Where(x => x.Title == taskTitle).FirstOrDefault();
            var user = userService.FindByEmail(assignTo);
            if (user != null)
            {
                if (!project.Clients.Contains(user))
                {
                    project.Clients.Add(user);
                    projectService.Update(project);
                }
                task.AssignedToId = user.Id;
                var opDetails = taskService.Update(task);
                return Result(opDetails);
            }
            return Json(new { success = false, responseText = "User was not found" }, JsonRequestBehavior.AllowGet);
        }

        private ActionResult Result(OperationDetails opDetails)
        {
            if (opDetails.Succedeed)
            {
                TempData["Result"] = "Succeed";
                TempData["Message"] = opDetails.Message;
                return PartialView("Result");
            }
            else
            {
                TempData["Result"] = "Failed";
                TempData["Message"] = opDetails.Message;
                return PartialView("Result");
            }
        }
    }
}