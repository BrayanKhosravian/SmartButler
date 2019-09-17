using System;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace SmartButler.Services.Registrable
{
    public interface INavigationService
    {
        Task<Page> PopAsync(bool animated = false);

        Task<Page> PopModalAsync(bool animated = false);

        Task PushAsync<TView>(bool animated = false)
            where TView : Page;

        Task PushModalAsync<TView>(bool animated = false)
            where TView : Page;

    }

    public class NavigationService : INavigationService
    {
        private readonly IPageRepository _pageRepository;
        private readonly Lazy<INavigation> _navigation;

        public NavigationService(IPageRepository pageRepository, System.Lazy<INavigation> navigation)
        {
            _pageRepository = pageRepository;
            _navigation = navigation;
        }

        public Task<Page> PopAsync(bool animated = false)
        {
            return _navigation.Value.PopAsync(animated);
        }

        public Task<Page> PopModalAsync(bool animated = false)
        {
            return _navigation.Value.PopModalAsync(animated);

        }

        public Task PushAsync<TView>(bool animated = false) where TView : Page
        {
            var page = _pageRepository.Resolve<TView>();

            return _navigation.Value.PushAsync(page, animated);
        }

        public Task PushModalAsync<TView>(bool animated = false) where TView : Page
        {
            var page = _pageRepository.Resolve<TView>();

            return _navigation.Value.PushModalAsync(page, animated);

        }

    }
}
