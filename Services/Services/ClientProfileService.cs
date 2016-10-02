using System;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;

using DomainEF.UnitOfWork;
using Services.Interfaces;
using Services.Helpers;
using ServiceEntities;


namespace Services.Services
{
    class ClientProfileService : IClientProfileService
    {
        private UnitOfWork uow;

        public OperationDetails Create(ClientProfile item)
        {
            OperationDetails details;
            bool result;
            try
            {
                var profile = Mapper.Map<DomainEntities.ClientProfile>(item);
                using (uow = new UnitOfWork())
                {
                    uow.ClientManager.Create(profile);
                    uow.SaveChanges(out result);
                }
                return new OperationDetails(result, result == true ? "Operation succed" : "Operation failed", "");
            }
            catch (AutoMapperMappingException ex)
            {
                return details = new OperationDetails(false, ex.Message, "");
            }
            catch (Exception ex)
            {
                return details = new OperationDetails(false, ex.Message, "");
            }
        }

        public ClientProfile Find(int id)
        {
            throw new NotImplementedException("Client profile demend id as a string");
        }

        public IEnumerable<ClientProfile> GetAll()
        {
            using (uow = new UnitOfWork())
            {
                var domainProfiles = uow.UserManager.Users.Select(x => x.ClientProfile).ToList();
                return Mapper.Map<IEnumerable<ClientProfile>>(domainProfiles);
            }
        }

        public void Dispose()
        {
            if (uow != null)
                uow.Dispose();
        }

        public OperationDetails Update(ClientProfile item)
        {
            throw new NotImplementedException();
        }

        public OperationDetails Delete(ClientProfile item)
        {
            throw new NotImplementedException();
        }
    }
}
