using System;

namespace UnitOfWork
{
    interface IUnitOfWork : IDisposable
    {
        void SaveChanges();
    }
}
