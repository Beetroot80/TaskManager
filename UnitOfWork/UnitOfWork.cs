using System;
using System.Data.Entity;

namespace UnitOfWork
{
    sealed class UnitOfWork : IUnitOfWork
    {
        private DbContext context;

        public UnitOfWork(DbContext context)
        {
            this.context = context;                
        }
        public void SaveChanges()
        {
            context.SaveChanges();
        }

        #region IDisposable Support
        private bool disposedValue = false;

        void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    context.Dispose();
                    context = null;
                }
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
