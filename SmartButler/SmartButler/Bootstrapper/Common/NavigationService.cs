using System;
using System.Threading.Tasks;
using SmartButler.Logic.Common;
using SmartButler.Logic.Interfaces;
using Xamarin.Forms;

namespace SmartButler.Bootstrapper.Common
{
	public class NavigationService : INavigationService
    {
        private readonly IPageRegistrar _pageRegistrar;
        private readonly Lazy<INavigation> _navigation;

        public NavigationService(IPageRegistrar pageRegistrar, System.Lazy<INavigation> navigation)
        {
            _pageRegistrar = pageRegistrar;
            _navigation = navigation;
        }

        public Task PopAsync(bool animated = false)
        {
            return _navigation.Value.PopAsync(animated);
           
        }

        public Task PopToRootAsync(bool animated = false)
        {
            return _navigation.Value.PopToRootAsync(animated);
        }

        public Task PopModalAsync(bool animated = false)
        {
            return _navigation.Value.PopModalAsync(animated);

        }

        public Task PushAsync<TViewModel>(bool animated = false) where TViewModel : BaseViewModel
        {
            var page = _pageRegistrar.Resolve<TViewModel>();

            return _navigation.Value.PushAsync(page, animated);
        }

        public Task PushModalAsync<TViewModel>(bool animated = false) where TViewModel : BaseViewModel
        {
            var page = _pageRegistrar.Resolve<TViewModel>();

            return _navigation.Value.PushModalAsync(page, animated);

        }

    }
}
