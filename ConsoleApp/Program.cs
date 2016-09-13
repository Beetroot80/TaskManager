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

            var personalInfo = new PersonalInfo()
            {
                FirstName = "Eugene",
                LastName = "Beetroot"
            };
            var user = new User()
            {
                PersonalInfoId = 0
            };

            context.Users.Add(user);
            context.SaveChanges();
            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
