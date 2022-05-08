using System;

namespace ActiveCampaign
{
    /// <summary>
    /// Base class for disposable objects
    /// </summary>
    public abstract class DisposableBase : IDisposable
    {
        protected bool disposed = false;

        /// <summary>
        /// Cleanup
        /// </summary>
        protected abstract void ReleaseResources();

        protected void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
                ReleaseResources();
                disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
