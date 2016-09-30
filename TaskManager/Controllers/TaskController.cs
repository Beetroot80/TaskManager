using AutoMapper;
using Microsoft.AspNet.Identity;
using ServiceEntities;
using Services;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    public class TaskController : Controller
    {
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

        [Authorize(Roles = "Administrator,Manager,User")]
        [HttpGet]
        public ActionResult AddTask(string projectTitle)
        {
            TempData["ProjectTitles"] = new List<string>();
            var taskModel = new ViewTasksModel();
            if (projectTitle == null)
            {
                var projectService = new ProjectService();
                var projectTitles = projectService.GetUserProjects(User.Identity.GetUserId()).Select(x => x.Title).ToList();
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

            serviceTask = Mapper.Map<ServiceTask>(model);
            serviceTask.CreatedById = User.Identity.GetUserId();
            serviceTask.CreationDate = DateTime.Now.Date;
            serviceTask.DeadLine = DateTime.Now.Date;
            serviceTask.ProjectId = projectService.FindByTitle(model.ProjectTitle).Id;
            serviceTask.StatusId = statusService.FindByTitle(model.StatusTitle).Id;
            serviceTask.PriorityId = priorityService.FindByTitle(model.PriorityTitle).Id;
            if (User.IsInRole("User"))
            {
                serviceTask.AssignedToId = User.Identity.GetUserId();
            }
            else
            {
                serviceTask.AssignedToId = userService.FindByEmail(model.AssignedToEmail).Id;
            }

            taskService.AddTask(serviceTask);
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Administrator, Manager, User")]
        [HttpPost]
        public ActionResult TaskList(string projectTitle)
        {
            var projectService = new ProjectService();
            var taskTitles = projectService.FindByTitle(projectTitle).Tasks.Select(x => x.Title).ToList();
            SelectList titles = new SelectList(taskTitles, "Title", "Task title", 0);
            return Json(taskTitles);
        }
    }
}