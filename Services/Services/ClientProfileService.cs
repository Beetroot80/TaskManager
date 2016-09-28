using ServiceEntities;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DomainEF.UnitOfWork;

namespace Services.Services
{
    class ClientProfileService
    {
        public static List<ClientProfile> GetAllUsers()
        {
            using (var uow = new UnitOfWork())
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
