using System;

namespace SmartButler.Services
{
    public abstract class DisposableService
    {
        public virtual bool ShouldDispose { get; set; }

        public abstract void Dispose();

        protected void Dispose(params IDisposable[] disposables)
        {
            foreach (var disposable in disposables)
            {
                 disposable?.Dispose();
            }
        }
    }
}
