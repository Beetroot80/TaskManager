using System;

namespace DomainEF.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void SaveChanges(out bool? result);
        void SaveChanges();
    }
}
