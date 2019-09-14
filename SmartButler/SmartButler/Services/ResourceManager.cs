using Autofac;

namespace SmartButler.Services
{
    public interface IResourceManager
    {
        void Dispose<TService>()
            where TService : DisposableService;
    }

    public class ResourceManager : IResourceManager
    {
        private IComponentContext _componentContext;

        public ResourceManager(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }

        public void Dispose<TService>()
            where TService : DisposableService
        {
            var service = (DisposableService)_componentContext.Resolve<TService>();
            if(service is DisposableService disposable && disposable.ShouldDispose)
                disposable.Dispose();
                
        }
    }
}
