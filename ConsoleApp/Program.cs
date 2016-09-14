using System;
using DomainEF;
using DomainCore;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string [] args)
        {
            TestConnactionAndCreationForDb();
        }

        static void TestConnactionAndCreationForDb()
        {
            var project = new Project()
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
            Console.ReadLine();
        }
    }
}
