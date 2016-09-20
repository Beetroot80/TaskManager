using System;
using DomainEF;

namespace UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IContext Context { get; }
        void SaveChanges();
    }
}
