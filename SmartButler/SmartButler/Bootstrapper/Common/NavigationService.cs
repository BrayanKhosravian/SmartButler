using System;
using System.Linq;
using System.Reactive.Threading.Tasks;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using ReactiveUI;
using SmartButler.Framework.Common;
using SmartButler.Logic.Common;
using SmartButler.Logic.Interfaces;
using Xamarin.Forms;

namespace SmartButler.Bootstrapper.Common
{
	public class NavigationService : INavigationService
    {
        private readonly IPageRegistrar _pageRegistrar;
        private readonly Lazy<INavigation> _navigation;

        public NavigationService(IPageRegistrar pageRegistrar, Lazy<INavigation> navigation)
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

        public Task PushAsync<TViewModel>(Parameter parameter, bool animated = false) 
	        where TViewModel : BaseViewModel
        {
	        var page = _pageRegistrar.Resolve<TViewModel>(parameter);

	        return _navigation.Value.PushAsync(page, animated);
        }

        public Task PushAsync<TViewModel>(Parameter[] parameters, bool animated = false) 
	        where TViewModel : BaseViewModel
        {
	        if(parameters == null || parameters.Length == 0)
                throw ExceptionFactory.Get<ArgumentNullException>("'paremters' was null or empty");
            if (parameters.Length == 1)
		        return PushAsync<TViewModel>(parameters[0], animated);

            var page = _pageRegistrar.Resolve<TViewModel>(parameters);

            return _navigation.Value.PushAsync(page, animated);
        }

        public Task PushModalAsync<TViewModel>(bool animated = false) where TViewModel : BaseViewModel
        {
            var page = _pageRegistrar.Resolve<TViewModel>();

            return _navigation.Value.PushModalAsync(page, animated);

        }

    }
}
