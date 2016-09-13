using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            TaskManagerContext context = new TaskManagerContext();

            var project = new Project()
            {
                Id = 0,
                Description = "Desc",
                Title = "Title"
            };

            context.Projects.Add(project);
            context.SaveChanges();
            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
