using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using SmartButler.Services.Registrable;
using SmartButler.ViewModels;
using SmartButler.Views;
using Xamarin.Forms;

namespace SmartButler.Bootstrapper
{
    public sealed class Bootstrapper
    {
        private readonly App _app;

        public Bootstrapper(App app)
        {
            _app = app;
        }

        public void Load(IDictionary<Type,Type> platformTypes = null)
        {
            var builder = new ContainerBuilder();

            // register types from other platforms to the IOC container
            if (platformTypes != null && platformTypes.Count > 0)
            {
                foreach (var kvp in platformTypes)
                {
                    builder.RegisterType(kvp.Key).As(kvp.Value);
                }
            }

            builder.RegisterModule<PageModule>();
            builder.RegisterModule<ServiceModule>();

            var container = builder.Build();

            var pageRepository = container.Resolve<IPageRepository>();
            pageRepository.Register<BluetoothDevicesListView, BluetoothDevicesViewModel>();
            pageRepository.Register<BottlesListView, BottlesViewModel>();
            pageRepository.Register<DrinkSelectionListView, DrinkSelectionViewModel>();
            pageRepository.Register<SelectionPage, SelectionPageViewModel>();
            pageRepository.Register<WelcomePage, WelcomePageViewModel>();

            var mainPage = pageRepository.Resolve<WelcomePage>();
            _app.MainPage = new NavigationPage(mainPage);

            // application.MainPage = 
        }

    }
}
