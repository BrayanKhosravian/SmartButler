using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using SmartButler.Bootstrapper.Modules;
using SmartButler.Services.Registrable;
using SmartButler.ViewModels;
using SmartButler.Views;
using SmartButler.Views.Registerable;
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

           

            builder.RegisterModule<PageComponentsModule>();
            builder.RegisterModule<ServiceModule>();

            var container = builder.Build();

            var pageRegistrar = container.Resolve<IPageRegistrar>();
            // register workspace component view models
            
            // register view and view model relationship
            pageRegistrar.Register<BluetoothPage, BluetoothPageViewModel>();
            pageRegistrar.Register<BottlesPage, BottlesPageViewModel>();
            pageRegistrar.Register<DrinksPage, DrinksPageViewModel>();
            pageRegistrar.Register<WelcomePage, WelcomePageViewModel>();
            pageRegistrar.Register<MakeDrinkPage, MakeDrinkPageViewModel>();
            pageRegistrar.Register<SettingsPage, SettingsPageViewModel>();

            var mainPage = pageRegistrar.Resolve<WelcomePage>();
          
            _app.MainPage = new NavigationPage(mainPage);

            // application.MainPage = 
        }

    }
}
