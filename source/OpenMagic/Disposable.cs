using System;

namespace OpenMagic
{
    public abstract class Disposable : IDisposable
    {
        private bool _isDisposed;

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing">
        ///     <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.
        /// </param>
        private void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            if (disposing)
            {
                DisposeManagedResources();
            }

            DisposeUnmanagedResources();

            _isDisposed = true;
        }

        /// <summary>
        ///     Releases (disposes) managed resources
        /// </summary>
        protected virtual void DisposeManagedResources()
        {
        }

        /// <summary>
        ///     Releases unmanaged resources
        /// </summary>
        protected virtual void DisposeUnmanagedResources()
        {
        }
    }
}