using System;
using DomainEF;
using DomainCore;
using ServiceEntities;
using Services;
using System.Collections.Generic;
using ServiceMapper;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string [] args)
        {
            MapperConfig.ConfigureAutoMapper();
            TestConnactionAndCreationForDb();
            var list = TestTaskServiceGetAll();
            int i = 0;
            Console.ReadLine();
        }

        static IEnumerable<ServiceTask> TestTaskServiceGetAll()
        {
            Console.WriteLine("GetAllStarted");
            return TaskService.GetAllTasks();
        }
        static void TestConnactionAndCreationForDb()
        {
            var project = new DomainCore.Project()
            {
                Description = "Desc2",
                Title = "Title2"
            };
            using (TaskManagerContext context = new TaskManagerContext())
            {
                context.Projects.Add(project);
                context.SaveChanges();
            }
            Console.WriteLine("Done");
        }
    }
}
