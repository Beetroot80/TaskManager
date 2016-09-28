using System;

using DomainEntities;

namespace DomainEF.Interfaces
{
    public interface IClientManager : IDisposable
    {
        void Create(ClientProfile item);
    }
}
