using System;
using System.Linq;
using System.Linq.Expressions;
using DomainCore;
using Repositories.Interfaces;
using DomainEF.Interfaces;
using UnitOfWork;
using System.Data.Entity;
using System.Collections.Generic;

namespace DomainEF.Repositories
{
    public class ClientProfileRepository : IClientProfileRepository
    {
        private readonly ITaskManagerContext context;

        public ClientProfileRepository(IUnitOfWork uow)
        {
            this.context = uow.Context as ITaskManagerContext;
        }

        public IEnumerable<ClientProfile> All()
        {
            return context.ClientProfiles.AsQueryable();
        }

        public IEnumerable<ClientProfile> AllIncluding(params Expression<Func<ClientProfile, object>>[] includeProperties)
        {
            IQueryable<ClientProfile> query = context.ClientProfiles;
            foreach (var i in includeProperties)
            {
                query = query.Include(i);
            }
            return query.ToList();
        }

        public void Delete(int id)
        {
            var clientProfile = context.ClientProfiles.Find(id);
            context.ClientProfiles.Remove(clientProfile);
        }

        public ClientProfile Find(int id)
        {
            return context.ClientProfiles.Find(id);
        }

        public void InsertOrUpdate(ClientProfile entity)
        {
            if (entity.Id == default(string)) //New
            {
                context.SetAdded(entity);
            }
            else //Excisting
            {
                context.SetModified(entity);
            }
        }

        #region IDisposable Support

        public void Dispose()
        {
            context.Dispose();
        }
        #endregion
    }
}
