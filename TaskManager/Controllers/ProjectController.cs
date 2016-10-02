using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using AutoMapper;

using ServiceEntities;
using Services.Services;
using Services;
using Services.Helpers;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    public class ProjectController : Controller
    {

        // GET: Project
        public ActionResult Index()
        {
            ProjectService pService = new ProjectService();
            IEnumerable<Project> projects = pService.GetAll();
            List<ProjectModel> projectModels = new List<ProjectModel>();

            foreach (var project in projects)
            {
                projectModels.Add(Mapper.Map<ProjectModel>(project));
            }
            return PartialView(projectModels.AsEnumerable());
        }

        //GET: Projects
        [Authorize(Roles = "Administrator,Manager,User")]
        [HttpGet]
        public ActionResult Projects()
        {
            var projectService = new ProjectService();
            var userId = User.Identity.GetUserId();
            var projects = projectService.GetProjectsWithCounters(userId);
            var projectModels = Mapper.Map<IEnumerable<ProjectModel>>(projects);

            return PartialView(projectModels.AsEnumerable());
        }

        //POST: Projects
        [Authorize(Roles = "Administrator,Manager,User")]
        [HttpPost]
        public ActionResult ViewTask(int projectId)
        {
            var taskService = new TaskService();
            var projectService = new ProjectService();
            var tasks = taskService.GetSignedTasks(projectId);
            List<ViewTasksModel> taskModels = new List<ViewTasksModel>();
            foreach (var task in tasks)
            {
                taskModels.Add(Mapper.Map<ViewTasksModel>(task));
            }
            ViewBag.ProjectTitle = projectService.Find(projectId).Title ?? "--------";
            return PartialView(taskModels.AsEnumerable());
        }

        //GET: Add project
        [Authorize(Roles = "Administrator,Manager,User")]
        [HttpGet]
        public ActionResult AddProject()
        {
            return PartialView();
        }

        //POST: Add project
        [Authorize(Roles = "Administrator,Manager,User")]
        [HttpPost]
        public ActionResult AddProject(ProjectModel model)
        {
            model.CreatedById = HttpContext.User.Identity.GetUserId();
            var projectService = new ProjectService();
            var opDetails = projectService.Create(Mapper.Map<ServiceEntities.Project>(model));
            return Result(opDetails);
        }

        //GET: Comments
        [Authorize(Roles = "Administrator,Manager,User")]
        public ActionResult Comments(int? Id)
        {
            if (Id != null)
            {
                var taskService = new TaskService();
                var taskComments = taskService.GetComments((int)Id);

                return PartialView(Mapper.Map<List<CommentModel>>(taskComments));
            }
            return RedirectToAction("Index", "Home");
        }

        //GET: Add task
        [Authorize(Roles = "Administrator,Manager,User")]
        [HttpGet]
        public ActionResult AddTask(string projectTitle)
        {
            TempData["ProjectTitles"] = new List<string>();
            var taskModel = new ViewTasksModel();
            if (projectTitle == null)
            {
                var projectService = new ProjectService();
                var projectTitles = projectService.GetAll(User.Identity.GetUserId()).Select(x => x.Title).ToList();
                if (projectTitles.Count == 0)
                    return RedirectToAction("AddProject", "Project");
                TempData["ProjectTitles"] = projectTitles;
            }
            else
            {
                TempData["ProjectTitles"] = new List<string>() { projectTitle };
            }
            var statusService = new StatusService();
            var priorityService = new PriorityService();
            var statusList = statusService.GetAll().Select(x => x.Title).ToList();
            var priorityList = priorityService.GetAll().Select(x => x.Title).ToList();

            TempData["StatusList"] = statusList;
            TempData["PriorityList"] = priorityList;

            return PartialView(taskModel);
        }

        //POST: Add task
        [Authorize(Roles = "Administrator,Manager,User")]
        [HttpPost]
        public ActionResult AddTask(ViewTasksModel model)
        {
            var serviceTask = new ServiceTask();
            var priorityService = new PriorityService();
            var statusService = new StatusService();
            var projectService = new ProjectService();
            var taskService = new TaskService();
            var userService = new UserService();
            var statusList = statusService.GetAll().Select(x => x.Title).ToList();
            var priorityList = priorityService.GetAll().Select(x => x.Title).ToList();

            TempData["StatusList"] = statusList;
            TempData["PriorityList"] = priorityList;

            serviceTask = Mapper.Map<ServiceTask>(model);
            serviceTask.CreatedById = User.Identity.GetUserId();
            serviceTask.CreationDate = DateTime.Now.Date;
            if (model.DeadLine < DateTime.Now.Date)
            {
                ModelState.AddModelError("DeadLine", "Incorrect date");
                TempData["ProjectTitles"] = projectService.GetAll(User.Identity.GetUserId()).Select(x => x.Title).ToList();
                return View(model);
            }
            if(!statusList.Contains(model.StatusTitle) || !priorityList.Contains(model.PriorityTitle))
            {
                TempData["ProjectTitles"] = projectService.GetAll(User.Identity.GetUserId()).Select(x => x.Title).ToList();
                ModelState.AddModelError("PriorityTitle", "Incorrect data");
                ModelState.AddModelError("StatusTitle", "Incorrect data");
                return View(model);
            }
            serviceTask.DeadLine = model.DeadLine ?? DateTime.Now.Date;
            serviceTask.ProjectId = projectService.FindByTitle(model.ProjectTitle).Id;
            serviceTask.StatusId = statusService.FindByTitle(model.StatusTitle).Id;
            serviceTask.PriorityId = priorityService.FindByTitle(model.PriorityTitle).Id;

            if (User.IsInRole("User"))
            {
                serviceTask.AssignedToId = User.Identity.GetUserId();
            }
            else
            {
                if (model.AssignedToEmail != null)
                    serviceTask.AssignedToId = userService.FindByEmail(model.AssignedToEmail).Id;
            }

            var opDetails = taskService.Create(serviceTask);
            return Result(opDetails);

        }

        //GET: Users
        [Authorize(Roles = "Administrator, Manager")]
        public ActionResult UserList()
        {
            return RedirectToAction("UserEmails", "Account");
        }

        //GET: Manage
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
            TempData ["ProjectTitles"] = projectService.GetAll(User.Identity.GetUserId()).Select(x => x.Title).ToList();
            return PartialView("Manage", manageModel);
        }

        //Post
        [Authorize(Roles = "Administrator, Manager, User")]
        [HttpPost]
        public ActionResult Manage(ManageModel model)
        {
            return PartialView("Manage");
        }

        [Authorize(Roles = "Administrator, Manager, User")]
        [HttpGet]
        public ActionResult ProjectList()
        {
            var projectService = new ProjectService();
            var projectTitles = projectService.GetAll(User.Identity.GetUserId()).Select(x => x.Title).ToList();
            return Json(projectTitles, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Administrator, Manager, User")]
        [HttpPost]
        public ActionResult TaskList(string projectTitle)
        {
            var projectService = new ProjectService();
            var taskService = new TaskService();
            try
            {
                var taskTitles = new List<string>();
                var project = projectService.GetProjectWithTaskByTitle(projectTitle);
                if (project != null)
                    taskTitles = project.Tasks.Select(x => x.Title).ToList();
                return Json(taskTitles);
            }
            catch (NullReferenceException)
            {
                return Json("");
            }
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