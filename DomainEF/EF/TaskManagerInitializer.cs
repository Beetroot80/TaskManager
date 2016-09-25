using System.Collections.Generic;
using System.Data.Entity;
using DomainCore;
using Microsoft.AspNet.Identity;
using DomainEF.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DomainEF
{
    class TaskManagerInitializer : DropCreateDatabaseAlways<TaskManagerContext>
    {
        protected override void Seed(TaskManagerContext context)
        {
            //Statuses
            var s0 = new Status() { Title = "NotAsigned" };
            var s1 = new Status() { Title = "InProgress" };
            var s2 = new Status() { Title = "Done" };

            context.Status.Add(s0);
            context.Status.Add(s1);
            context.Status.Add(s2);
            context.SaveChanges();

            //Priorities
            var p0 = new Priority() { Title = "High" };
            var p1 = new Priority() { Title = "Low" };
            var p2 = new Priority() { Title = "Minor" };
            context.Priorities.Add(p0);
            context.Priorities.Add(p1);
            context.Priorities.Add(p2);
            context.SaveChanges();

            //Roles
            var RoleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(context));
            var admin = new ApplicationRole { Name = "Administrator" };
            var manager = new ApplicationRole { Name = "Manager" };
            var user = new ApplicationRole { Name = "User" };
            RoleManager.Create(admin);
            RoleManager.Create(manager);
            RoleManager.Create(user);

            //Users
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            UserManager.Create(new ApplicationUser
            {
                UserName = "Eugene@gmail.com",
                Email = "Eugene@gmail.com",
                ClientProfile = new ClientProfile { Name = "Eugene", Surname = "Beetroot" }
            }, "Eugene1!");

            UserManager.Create(new ApplicationUser
            {
                UserName = "Kate@gmail.com",
                Email = "Kate@gmail.com",
                ClientProfile = new ClientProfile { Name = "Kate", Surname = "Windstorm" }
            }, "Kate11!");

            var u0 = UserManager.Find("Eugene@gmail.com", "Eugene1!");
            var u1 = UserManager.Find("Kate@gmail.com", "Kate11!");
            UserManager.AddToRole(u0.Id, admin.Name);
            UserManager.AddToRole(u1.Id, admin.Name);
            var userGroup0 = new List<ApplicationUser>();
            var userGroup1 = new List<ApplicationUser>();
            userGroup0.Add(u0);
            userGroup0.Add(u1);
            userGroup1.Add(u0);


            //Projects
            var pr0 = new Project() { Title = "FirstProject", Description = "This project was created first", Clients = userGroup0, CreatedBy = u0 };
            var pr1 = new Project() { Title = "SecondProject", Description = "This project was created second", Clients = userGroup1, CreatedBy = u0 };
            context.Projects.Add(pr0);
            context.Projects.Add(pr1);
            context.SaveChanges();

            //Tasks
            var t0 = new DomainTask() { Title = "Task1", Description = "First task", Status = s0, Priority = p0, Project = pr0, CreatedBy = u1, CreatedBy_Id = u1.Id };
            var t1 = new DomainTask() { Title = "Task2", Description = "Second task", Status = s0, Priority = p1, Project = pr0, CreatedBy = u0, CreatedBy_Id = u0.Id };
            var t2 = new DomainTask() { Title = "Task3", Description = "Third task", Status = s1, Priority = p1, Project = pr1, CreatedBy = u0, CreatedBy_Id = u0.Id };
            var t3 = new DomainTask() { Title = "Task4", Description = "Fourth task", Status = s2, Priority = p2, Project = pr1, CreatedBy = u0, CreatedBy_Id = u0.Id };
            context.Tasks.Add(t0);
            context.Tasks.Add(t1);
            context.Tasks.Add(t2);
            context.Tasks.Add(t3);
            context.SaveChanges();

            //Comments
            var c0 = new Comment() { Text = "My first comment", DomainTask = t0, Client = u0 };
            var c1 = new Comment() { Text = "Hello world", DomainTask = t1, Client = u0 };
            context.Comments.Add(c0);
            context.Comments.Add(c1);
            context.Entry<ApplicationUser>(u0).State = EntityState.Modified;
            context.Entry<ApplicationUser>(u1).State = EntityState.Modified;
            context.Entry<DomainTask>(t0).State = EntityState.Modified;
            context.Entry<DomainTask>(t1).State = EntityState.Modified;
            context.SaveChanges();

        }
    }
}
