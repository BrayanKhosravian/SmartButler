namespace SmartButler.Services
{
    public abstract class DisposableService
    {
        public abstract bool ShouldDispose { get; set; }

        public abstract void Dispose();
    }
}
