namespace SmartButler.Services
{
    public abstract class DisposableService
    {
        public virtual bool ShouldDispose { get; set; }

        public abstract void Dispose();
    }
}
