using System;

namespace UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        void SaveChanges();
    }
}
