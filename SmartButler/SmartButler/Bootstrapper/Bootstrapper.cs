using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using SmartButler.Bootstrapper.Common;
using SmartButler.Bootstrapper.Modules;
using SmartButler.DataAccess.Models;
using SmartButler.DataAccess.Repositories;
using SmartButler.Framework.Resources;
using SmartButler.Logic.ModelTemplates.Drinks;
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

        public async Task Load(ContainerBuilder builder)
        {
	        builder.RegisterModule<PageComponentsModule>();
            builder.RegisterModule<ServiceModule>();
            builder.RegisterModule<RepositoryModule>();
			
            var container = builder.Build();
            var pageRegistrar = container.Resolve<IPageRegistrar>();
            RegisterPages(pageRegistrar);

            // configure database
			var ingredientFactory = new IngredientsFactory(new IngredientBuilder());
			var defaultIngredients = ingredientFactory.GetDefaultIngredients();
			await container.Resolve<IIngredientsRepository>().ConfigureAsync(defaultIngredients);

			var drinkRecipesFactory = new DrinkRecipeFactory(new DrinkRecipeBuilder());
			var defaultDrinks = drinkRecipesFactory.GetDefaultDrinks();
			await container.Resolve<IDrinkRecipesRepository>().ConfigureAsync(defaultDrinks);

			var mainPage = pageRegistrar.Resolve<WelcomePageViewModel>();
          
            _app.MainPage = new NavigationPage(mainPage);	

        }

        private void RegisterPages(IPageRegistrar pageRegistrar)
        {
	        pageRegistrar.Register<WelcomePageViewModel, View.Pages.WelcomePage>();
	        pageRegistrar.Register<SettingsPageViewModel, View.Pages.SettingsPage>();
	        pageRegistrar.Register<BluetoothPageViewModel, View.Pages.BluetoothPage>();
	        pageRegistrar.Register<DrinksPageViewModel, View.Pages.DrinksPage>();
	        pageRegistrar.Register<MakeDrinkPageViewModel, View.Pages.MakeDrinkPage>();

	        pageRegistrar.Register<ShowIngredientsPageViewModel, IngredientsPage>();
	        pageRegistrar.Register<SelectIngredientsPageViewModel, IngredientsPage>();

	        pageRegistrar.Register<EditIngredientPageViewModel, ConfigureIngredientPage>();
	        pageRegistrar.Register<AddIngredientPageViewModel, ConfigureIngredientPage>();

	        pageRegistrar.Register<EditDrinkRecipePageViewModel, View.Pages.EditDrinkRecipePage>();
        }

    }
}
