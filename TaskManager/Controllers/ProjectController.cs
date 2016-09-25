using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
        public ActionResult Projects() //TODO: display only my projects
        {
            var projectService = new ProjectService();
            var projects = projectService.GetAllProjectsWithCounts();
            List<ProjectModel> projectModels = new List<ProjectModel>();
            foreach(var project in projects)
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
            foreach(var task in tasks)
            {
                taskModels.Add(Mapper.Map<ViewTasksModel>(task));
            }
            ViewBag.CreatedByEmail = tasks.Select(x => x.Project.CreatedBy.Email).First(); //TODO: get projectby id;
            ViewBag.ProjectTitle = taskModels.Select(x => x.ProjectTitle).First();
            return PartialView(taskModels.AsEnumerable());
        }
        [Authorize(Roles = "Administrator,Manager,User")]
        [HttpPost]
        public ActionResult TaskInfo(int taskId)
        {
            return PartialView();
        }
        [Authorize(Roles = "Administrator,Manager,User")]
        [HttpGet]
        public ActionResult AddProject()
        {
            return PartialView();
        }

        [Authorize(Roles = "Administrator,Manager,User")]
        [HttpPost]
        public ActionResult AddProject(ProjectModel model)
        {
            var projectService = new ProjectService();
            projectService.Addproject(Mapper.Map<ServiceEntities.Project>(model));
            return PartialView();
        }

    }
}