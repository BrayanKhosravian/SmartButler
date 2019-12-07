using System.Threading.Tasks;
using Autofac;
using SmartButler.Bootstrapper.Common;
using SmartButler.Bootstrapper.Modules;
using SmartButler.DataAccess.Repositories;
using SmartButler.Framework.Resources;
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
			var ingredientFactory = new IngredientsFactory(new IngredientBuilder());
			await container.Resolve<IIngredientsRepository>().ConfigureAsync(ingredientFactory.GetDefaultIngredients());

			var drinkRecipesFactory = new DrinkRecipeFactory(new DrinkRecipeBuilder());
			await container.Resolve<IDrinkRecipesRepository>().ConfigureAsync(drinkRecipesFactory.GetDefaultDrinks());

			// register view and view model relationship
			var pageRegistrar = container.Resolve<IPageRegistrar>();

			pageRegistrar.Register<BluetoothPageViewModel, BluetoothPage>();
            pageRegistrar.Register<IngredientsPageViewModel, IngredientsPage>();
            pageRegistrar.Register<DrinksPageViewModel, DrinksPage>();
            pageRegistrar.Register<WelcomePageViewModel, WelcomePage>();
            pageRegistrar.Register<MakeDrinkPageViewModel, MakeDrinkPage>();
            pageRegistrar.Register<SettingsPageViewModel, SettingsPage>();
			pageRegistrar.Register<EditIngredientPageViewModel, EditIngredientPage>();

            var mainPage = pageRegistrar.Resolve<WelcomePageViewModel>();
          
            _app.MainPage = new NavigationPage(mainPage);

        }

    }
}
