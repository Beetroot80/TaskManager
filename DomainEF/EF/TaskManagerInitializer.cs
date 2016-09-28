using System.Collections.Generic;
using System.Data.Entity;
using DomainCore;
using Microsoft.AspNet.Identity;
using DomainEF.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace DomainEF
{
    class TaskManagerInitializer : DropCreateDatabaseIfModelChanges<TaskManagerContext>
    {
        protected override void Seed(TaskManagerContext context)
        {
            //Statuses
            var s0 = new Status() { Title = "NotStarted" };
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
            var userGroup2 = new List<ApplicationUser>();
            var userGroup3 = new List<ApplicationUser>();
            var userGroup4 = new List<ApplicationUser>();
            userGroup0.Add(u0);
            userGroup1.Add(u0);
            userGroup2.Add(u0);
            userGroup2.Add(u0);
            userGroup3.Add(u1);
            userGroup3.Add(u0);
            userGroup4.Add(u1);
            userGroup4.Add(u0);

            context.SaveChanges();

            //Projects

            var pr0 = new Project() { Title = "Home", Description = "Home tasks for me and my wife", Clients = userGroup0, CreatedBy = u0 };
            var pr1 = new Project() { Title = "Work", Description = "My boss can't hold his ideas", Clients = userGroup1, CreatedBy = u0 };
            var pr2 = new Project() { Title = "Home2", Description = "Home tasks for me and my wife", Clients = userGroup2, CreatedBy = u1 };
            var pr3 = new Project() { Title = "My game Ideas", Description = "damn, I wont my own game", Clients = userGroup3, CreatedBy = u1 };
            var pr4 = new Project() { Title = "Buy medicine", Description = "My grandma's needs", Clients = userGroup4, CreatedBy = u0 };
            context.Projects.Add(pr0);
            context.Projects.Add(pr1);
            context.Projects.Add(pr2);
            context.Projects.Add(pr3);
            context.Projects.Add(pr4);
            context.SaveChanges();

            //Tasks
            var t0 = new DomainTask() { Title = "Wash windows", Description = "Why should i wash windows and not she?", Status = s0, Priority = p0, Project = pr0, CreatedBy = u1, CreatedBy_Id = u1.Id };
            var t1 = new DomainTask() { Title = "Wash dishes", Description = "For real, again I should do it?", Status = s0, Priority = p1, Project = pr0, CreatedBy = u0, CreatedBy_Id = u0.Id };
            var t2 = new DomainTask() { Title = "Chapter 1", Description = "Create first chapter for child's book", Status = s1, Priority = p1, Project = pr1, CreatedBy = u0, CreatedBy_Id = u0.Id };
            var t3 = new DomainTask() { Title = "Fix printer", Description = "Bosses printer has broke down", Status = s2, Priority = p2, Project = pr1, CreatedBy = u0, CreatedBy_Id = u0.Id };
            var t4 = new DomainTask() { Title = "Buy dishwasher", Description = "Tired to wash dishes", Status = s0, Priority = p0, Project = pr2, CreatedBy = u1, CreatedBy_Id = u1.Id };
            var t5 = new DomainTask() { Title = "Buy window washer", Description = "Is it even exist?", Status = s0, Priority = p1, Project = pr2, CreatedBy = u0, CreatedBy_Id = u0.Id };
            var t6 = new DomainTask() { Title = "Buy new wife", Description = "Need to consider about this", Status = s1, Priority = p1, Project = pr2, CreatedBy = u0, CreatedBy_Id = u0.Id };
            var t7 = new DomainTask() { Title = "Main character", Description = "Sketches", Status = s2, Priority = p2, Project = pr3, CreatedBy = u0, CreatedBy_Id = u0.Id };
            var t8 = new DomainTask() { Title = "Environment", Description = "Design where this took place", Status = s2, Priority = p2, Project = pr3, CreatedBy = u0, CreatedBy_Id = u0.Id };
            var t9 = new DomainTask() { Title = "Classes", Description = "Design classes", Status = s2, Priority = p2, Project = pr3, CreatedBy = u0, CreatedBy_Id = u0.Id };
            context.Tasks.Add(t0);
            context.Tasks.Add(t1);
            context.Tasks.Add(t2);
            context.Tasks.Add(t3);
            context.Tasks.Add(t4);
            context.Tasks.Add(t5);
            context.Tasks.Add(t6);
            context.Tasks.Add(t7);
            context.Tasks.Add(t8);
            context.Tasks.Add(t9);
            context.SaveChanges();

            //Comments
            var c0 = new Comment() { Text = "My first comment", DomainTask = t0, Client = u0 };
            var c1 = new Comment() { Text = "Hello world", DomainTask = t1, Client = u0 };
            context.Comments.Add(c0);
            context.Comments.Add(c1);
            //context.Entry<ApplicationUser>(u0).State = EntityState.Modified;
            //context.Entry<ApplicationUser>(u1).State = EntityState.Modified;
            //context.Entry<DomainTask>(t0).State = EntityState.Modified;
            //context.Entry<DomainTask>(t1).State = EntityState.Modified;
            //context.Entry<DomainTask>(t2).State = EntityState.Modified;
            //context.Entry<DomainTask>(t3).State = EntityState.Modified;

            context.SaveChanges();

        }
    }
}
