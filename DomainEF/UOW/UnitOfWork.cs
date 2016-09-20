using System;
using System.Data.Entity;
using DomainEF;

namespace UnitOfWork
{
    public sealed class UnitOfWork<TContext> : IUnitOfWork
        where TContext : IContext, new()
    {
        private IContext context;
        public UnitOfWork()
        {
            this.context = new TContext();
        }
        public UnitOfWork(IContext context)
        {
            this.context = context;
        }
        public void SaveChanges()
        {
            context.SaveChanges();
        }
        public IContext Context
        {
            get
            {
                return (TContext)context;
            }
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
