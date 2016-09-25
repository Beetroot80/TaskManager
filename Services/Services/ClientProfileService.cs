using ServiceEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork;
using Repositories;
using DomainEF;
using AutoMapper;
using DomainEF.Repositories;


namespace Services.Services
{
    class ClientProfileService
    {
        public static List<ClientProfile> GetAllUsers()
        {
            using (var uow = new UnitOfWork<TaskManagerContext>())
            {
                    var clients = uow.UserManager.Users.Select(x => x.ClientProfile).ToList();
                    var serviceClients = new List<ClientProfile>();
                    foreach(var c in clients)
                    {
                        serviceClients.Add(Mapper.Map<ClientProfile>(c));
                    }
                    return serviceClients;                
            }
        }
    }
}
