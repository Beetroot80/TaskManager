using System.Collections.Generic;
using System.Data.Entity;
using DomainCore;

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

            //Personal info
            //var pi0 = new PersonalInfo() { FirstName = "Eugene", LastName = "Beetroot", UserId = 1 };
            //var pi1 = new PersonalInfo() { FirstName = "Amely", LastName = "Strange", UserId = 2 };
            //context.Entry<PersonalInfo>(pi0).State = EntityState.Added;
            //context.Entry<PersonalInfo>(pi1).State = EntityState.Added;
            //context.SaveChanges();

            //Users
            var u0 = new ClientProfile { Id = "1", Name = "Eugene" };
            var u1 = new ClientProfile() { Id = "2", Name = "Ann" };
            var userGroup0 = new List<ClientProfile>();
            var userGroup1 = new List<ClientProfile>();
            userGroup0.Add(u0);
            userGroup0.Add(u1);
            userGroup1.Add(u0);
            context.ClientProfiles.AddRange(userGroup0);
            context.ClientProfiles.AddRange(userGroup1);
            context.SaveChanges();

            //Projects
            var pr0 = new Project() { Title = "FirstProject", Description = "This project was created first", ClientProfiles = userGroup0 };
            var pr1 = new Project() { Title = "SecondProject", Description = "This project was creted second", ClientProfiles = userGroup1 };
            context.Projects.Add(pr0);
            context.Projects.Add(pr1);
            context.SaveChanges();

            //Tasks
            var t0 = new DomainTask() { Title = "Task1", Description = "First task", Status = s0, Priority = p0, Project = pr0, CreatedBy = u1, CreatedBy_Id = u1.Id };
            var t1 = new DomainTask() { Title = "Task2", Description = "Second task", Status = s0, Priority = p1, Project = pr0, CreatedBy = u0, CreatedBy_Id = u0.Id };
            var t2 = new DomainTask() { Title = "Task3", Description = "Third task", Status = s1, Priority = p1, Project = pr1, CreatedBy = u0, CreatedBy_Id = u0.Id };
            var t3 = new DomainTask() { Title = "Task4", Description = "Fourth tas", Status = s2, Priority = p2, Project = pr1, CreatedBy = u0, CreatedBy_Id = u0.Id };
            context.Tasks.Add(t0);
            context.Tasks.Add(t1);
            context.Tasks.Add(t2);
            context.Tasks.Add(t3);
            context.SaveChanges();

            //Comments
            var c0 = new Comment() { Text = "My first comment", DomainTask = t0, ClientProfile = u0 };
            var c1 = new Comment() { Text = "Hello world", DomainTask = t1, ClientProfile = u0 };
            context.Comments.Add(c0);
            context.Comments.Add(c1);
            context.Entry<ClientProfile>(u0).State = EntityState.Modified;
            context.Entry<ClientProfile>(u1).State = EntityState.Modified;
            context.Entry<DomainTask>(t0).State = EntityState.Modified;
            context.Entry<DomainTask>(t1).State = EntityState.Modified;
            context.SaveChanges();

        }
    }
}
