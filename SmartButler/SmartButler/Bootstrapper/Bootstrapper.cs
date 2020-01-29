using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using Plugin.Media;
using SmartButler.Bootstrapper.Common;
using SmartButler.Bootstrapper.Modules;
using SmartButler.DataAccess.Repositories;
using SmartButler.Logic.Services;
using SmartButler.Logic.ViewModels;
using SmartButler.View.Pages;
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

        public async Task LoadAsync(ContainerBuilder builder)
        {
	        await InitializeThirdPartyFrameworks();

	        builder.RegisterModule<PageComponentsModule>();
            builder.RegisterModule<ServiceModule>();
            builder.RegisterModule<RepositoryModule>();
			
            var container = builder.Build();
            var pageRegistrar = container.Resolve<IPageRegistrar>();
            RegisterPages(pageRegistrar);
            
			await InitializeDataBaseAsync(container);

			var mainPage = pageRegistrar.Resolve<WelcomePageViewModel>();
          
            _app.MainPage = new NavigationPage(mainPage);	

        }

        private static Task InitializeThirdPartyFrameworks()
        {
	        var tasks = new List<Task>();
	        tasks.Add(CrossMedia.Current.Initialize());
	        // add other frameworks Inits to 'tasks'

	        return Task.WhenAll(tasks);
        }

        private static async Task InitializeDataBaseAsync(IContainer container)
        {
	        var ingredientFactory = new IngredientsFactory(new IngredientBuilder());
	        var defaultIngredients = ingredientFactory.GetDefaultIngredients();
	        await container.Resolve<IIngredientsRepository>().ConfigureAsync(defaultIngredients);

	        var drinkRecipesFactory = new DrinkRecipeFactory(new DrinkRecipeBuilder());
	        var defaultDrinks = drinkRecipesFactory.GetDefaultDrinks();
	        await container.Resolve<IDrinkRecipesRepository>().ConfigureAsync(defaultDrinks);
        }

        private static void RegisterPages(IPageRegistrar pageRegistrar)
        {
	        pageRegistrar.Register<WelcomePageViewModel, WelcomePage>();
	        pageRegistrar.Register<SettingsPageViewModel, SettingsPage>();
	        pageRegistrar.Register<BluetoothPageViewModel, BluetoothPage>();
	        pageRegistrar.Register<DrinksPageViewModel, DrinksPage>();
	        pageRegistrar.Register<MakeDrinkPageViewModel, MakeDrinkPage>();

	        pageRegistrar.Register<ShowIngredientsPageViewModel, IngredientsPage>();
	        pageRegistrar.Register<SelectIngredientsPageViewModel, IngredientsPage>();

	        pageRegistrar.Register<EditIngredientPageViewModel, ConfigureIngredientPage>();
	        pageRegistrar.Register<AddIngredientPageViewModel, ConfigureIngredientPage>();

	        pageRegistrar.Register<EditDrinkRecipePageViewModel, EditDrinkRecipePage>();
	        pageRegistrar.Register<AddDrinkRecipePageViewModel, EditDrinkRecipePage>();
        }

        

    }
}
