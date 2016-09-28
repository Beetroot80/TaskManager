using DomainEntities;
using DomainEF.Interfaces;

namespace DomainEF.Repositories
{
    class ClientManager : IClientManager
    {
        public ITaskManagerContext Context { get; set; }
        public ClientManager(ITaskManagerContext context)
        {
            Context = context;
        }

        public void Create(ClientProfile item)
        {
            Context.ClientProfiles.Add(item);
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
