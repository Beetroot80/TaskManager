using Microsoft.AspNet.Identity;
using Services;
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
            TempData["ProjectTitles"] = projectService.GetAll(User.Identity.GetUserId()).Select(x => x.Title).ToList();
            return PartialView("Manage", manageModel);
        }

        //This code is just awful, have to totally refactor this!
        [Authorize(Roles = "Administrator, Manager, User")]
        [HttpPost]
        public ActionResult Manage(string projectTitle, string taskTitle, string projectAction, string taskAction, string assignTo)
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
                if (isInAdminrole && adminsProjectAction.Contains(projectAction))
                {
                    switch (projectAction)
                    {
                        case "Delete project":
                            DeleteProject(projectTitle);
                            break;
                        case "Add user to project":
                            AddUserToProject(projectTitle, assignTo);
                            break;
                        case "Manage tasks":
                            if (adminsTaskAction.Contains(taskAction))
                            {
                                switch (taskAction)
                                {
                                    case "Assign to user":
                                        AssignToTask(projectTitle, taskTitle, assignTo);
                                        break;
                                    case "Delete":
                                        DeleteTask(projectTitle, taskTitle);
                                        break;
                                    default:
                                        return RedirectToAction("Manage");
                                }
                            }
                            else
                                ModelState.AddModelError("TaskActions", "Invalid task action");
                            break;
                        default:
                            return RedirectToAction("Manage");
                    }
                }
                else if (!isInAdminrole && userProjectAction.Contains(projectAction))
                {
                    switch (projectAction)
                    {
                        case "Delete":
                            DeleteProject(projectTitle);
                            break;
                        case "Manage tasks":
                            if (userTaskAction.Contains(taskAction))
                            {
                                switch (taskAction)
                                {
                                    case "Delete":
                                        DeleteTask(projectTitle, taskTitle);
                                        break;
                                    default:
                                        return RedirectToAction("Manage");
                                }
                            }
                            else
                                ModelState.AddModelError("TaskActions", "Invalid task action");
                            break;
                        default:
                            return RedirectToAction("Manage");
                    }
                }
                else
                    ModelState.AddModelError("ProjectActions", "Invalid project action");
            }
            return PartialView("Manage");
        }
        private ActionResult DeleteProject(string projectTitle)
        {
            var projectService = new ProjectService();
            var project = projectService.FindByTitle(projectTitle);
            var opDetails = projectService.Delete(project);
            return RedirectToAction("Index","Home");
        }
        private ActionResult DeleteTask(string projectTitle, string taskTitle)
        {
            var taskService = new TaskService();
            var task = taskService.GetAll().Where(x => x.Title == taskTitle && x.Project.Title == projectTitle).FirstOrDefault();
            var opDetails = taskService.Delete(task);
            return RedirectToAction("Index", "Home");
        }
        private ActionResult AddUserToProject(string projectTitle, string assignTo)
        {
            var projectService = new ProjectService();
            var userService = new UserService();
            var project = projectService.FindByTitle(projectTitle);
            var user = userService.FindByEmail(assignTo);
            if (user != null)
            {
                project.Clients.Add(user);
                projectService.Update(project);
            }
            return RedirectToAction("Index", "Home");
        }
        private ActionResult AssignToTask(string projectTitle, string taskTitle, string assignTo)
        {
            var projectService = new ProjectService();
            var userService = new UserService();
            var taskService = new TaskService();
            var project = projectService.FindByTitle(projectTitle);
            var task = taskService.GetAll(project.Id).Where(x => x.Title == taskTitle).FirstOrDefault();
            var user = userService.FindByEmail(assignTo);
            if (user != null)
            {
                task.AssignedToId = user.Id;
                taskService.Update(task);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}