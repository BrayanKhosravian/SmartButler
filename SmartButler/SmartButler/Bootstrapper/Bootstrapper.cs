using System.Threading.Tasks;
using Autofac;
using SmartButler.Bootstrapper.Common;
using SmartButler.Bootstrapper.Modules;
using SmartButler.DataAccess.Repositories;
using SmartButler.Logic.Services;
using SmartButler.Logic.ViewModels;
using SmartButler.View.Views;
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
            builder.RegisterModule<RepositoryModule>();

            var container = builder.Build();

			// configure database
			var ingredientFactory = new IngredientFactory(new IngredientBuilder());
			await container.Resolve<IIngredientRepository>().ConfigureAsync(ingredientFactory.GetDefaultIngredients());

			// register view and view model relationship
			var pageRegistrar = container.Resolve<IPageRegistrar>();

			pageRegistrar.Register<BluetoothPageViewModel, BluetoothPage>();
            pageRegistrar.Register<IngredientsPageViewModel, IngredientsPage>();
            pageRegistrar.Register<DrinksPageViewModel, DrinksPage>();
            pageRegistrar.Register<WelcomePageViewModel, WelcomePage>();
            pageRegistrar.Register<MakeDrinkPageViewModel, MakeDrinkPage>();
            pageRegistrar.Register<SettingsPageViewModel, SettingsPage>();

            var mainPage = pageRegistrar.Resolve<WelcomePageViewModel>();
          
            _app.MainPage = new NavigationPage(mainPage);

        }

    }
}
