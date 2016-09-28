using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using AutoMapper;
using ServiceEntities;
using ServiceMapper;
using Services.Services;
using TaskManager.Models;
using Services;

namespace TaskManager.Controllers
{
    public class ProjectController : Controller
    {

        // GET: Project
        public ActionResult Index()
        {
            ProjectService pService = new ProjectService();
            IEnumerable<Project> projects = pService.GetFullTasks();
            List<ProjectModel> projectModels = new List<ProjectModel>();

            foreach (var project in projects)
            {
                projectModels.Add(Mapper.Map<ProjectModel>(project));
            }
            return PartialView(projectModels.AsEnumerable());
        }

        [Authorize(Roles = "Administrator,Manager,User")]
        [HttpGet]
        public ActionResult Projects()
        {
            var projectService = new ProjectService();
            var userId = User.Identity.GetUserId();
            var projects = projectService.GetAllProjectsWithCounts(userId);
            List<ProjectModel> projectModels = new List<ProjectModel>();
            foreach (var project in projects)
            {
                projectModels.Add(Mapper.Map<ProjectModel>(project));
            }
            return PartialView(projectModels.AsEnumerable());
        }

        [Authorize(Roles = "Administrator,Manager,User")]
        [HttpPost]
        public ActionResult ViewTask(int projectId)
        {
            var taskService = new TaskService();
            var tasks = taskService.GetSignedTasks(projectId);
            List<ViewTasksModel> taskModels = new List<ViewTasksModel>();
            foreach (var task in tasks)
            {
                taskModels.Add(Mapper.Map<ViewTasksModel>(task));
            }
            ViewBag.CreatedByEmail = tasks.Select(x => x.Project.CreatedBy.Email).FirstOrDefault(); //TODO: get projectby id;
            ViewBag.ProjectTitle = taskModels.Select(x => x.ProjectTitle).FirstOrDefault();
            return PartialView(taskModels.AsEnumerable());
        }
        //[Authorize(Roles = "Administrator,Manager,User")]
        //[HttpPost]
        ////public ActionResult TaskInfo(int taskId)
        ////{
        ////    return PartialView();
        ////}
        [Authorize(Roles = "Administrator,Manager,User")]
        [HttpGet]
        public ActionResult AddProject()
        {
            return PartialView();
        }

        [Authorize(Roles = "Administrator,Manager,User")]
        [HttpPost]
        public ActionResult AddProject(ProjectModel model) //TODO: added or not?
        {
            model.CreatedById = HttpContext.User.Identity.GetUserId();
            var projectService = new ProjectService();
            projectService.Addproject(Mapper.Map<ServiceEntities.Project>(model));
            return RedirectToAction("Index", "Home");
        }

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

        [Authorize(Roles = "Administrator,Manager,User")]
        [HttpGet]
        public ActionResult AddTask(string projectTitle)
        {
            TempData["ProjectTitles"] = projectTitle;
            projectTitle = null;
            var taskModel = new ViewTasksModel();
            if (projectTitle == null)
            {
                var projectService = new ProjectService();
                var serviceProjects = projectService.GetUserProjects(User.Identity.GetUserId());
                var projectTitles = new List<string>();
                foreach(var item in serviceProjects)
                {
                    projectTitles.Add(item.Title);
                }
                TempData["ProjectTitles"] = projectTitles;
            }
            var infoService = new InfoService();
            var statusList = infoService.StatusList().Select(x => x.Title).ToList();
            var priorityList = infoService.PriorityList().Select(x => x.Title).ToList();

            TempData["StatusList"] = statusList;
            TempData["PriorityList"] = priorityList;

            return PartialView(taskModel);
        }

        [Authorize(Roles = "Administrator,Manager,User")]
        [HttpPost]
        public ActionResult AddTask(ViewTasksModel model)
        {
            //var userId = User.Identity.GetUserId();
            //var serviceModel = new ServiceTask();
            //serviceModel = Mapper.Map<ServiceTask>(model);
            //serviceModel.CreatedById = userId;

            //string projectTitle = null;
            //if (projectTitle == null)
            //{

            //}
            return RedirectToAction("Index", "Home");
        }


    }
}