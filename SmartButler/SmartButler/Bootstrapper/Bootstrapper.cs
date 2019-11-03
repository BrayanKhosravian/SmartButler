using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using SmartButler.Bootstrapper.Modules;
using SmartButler.Repositories;
using SmartButler.Services.Registrable;
using SmartButler.ViewModels;
using SmartButler.ViewModels.RegisterAble;
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

        public async Task Load(ContainerBuilder builder)
        {
	        builder.RegisterModule<PageComponentsModule>();
            builder.RegisterModule<ServiceModule>();

            var container = builder.Build();

			// configure app
			await container.Resolve<IIngredientRepository>().ConfigureAsync();

            var pageRegistrar = container.Resolve<IPageRegistrar>();

            // register view and view model relationship
            pageRegistrar.Register<BluetoothPage, BluetoothPageViewModel>();
            pageRegistrar.Register<IngredientsPage, IngredientsPageViewModel>();
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
