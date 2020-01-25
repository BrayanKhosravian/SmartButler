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

			// configure database
			var ingredientFactory = new IngredientsFactory(new IngredientBuilder());
			var defaultIngredients = ingredientFactory.GetDefaultIngredients();
			await container.Resolve<IIngredientsRepository>().ConfigureAsync(defaultIngredients);

			var drinkRecipesFactory = new DrinkRecipeFactory(new DrinkRecipeBuilder());
			var defaultDrinks = drinkRecipesFactory.GetDefaultDrinks();
			await container.Resolve<IDrinkRecipesRepository>().ConfigureAsync(defaultDrinks);
			
			// register view and view model relationship
			var pageRegistrar = container.Resolve<IPageRegistrar>();

			pageRegistrar.Register<BluetoothPageViewModel, BluetoothPage>();
            pageRegistrar.Register<IngredientsPageViewModel, IngredientsPage>();
            pageRegistrar.Register<DrinksPageViewModel, DrinksPage>();
            pageRegistrar.Register<WelcomePageViewModel, WelcomePage>();
            pageRegistrar.Register<MakeDrinkPageViewModel, MakeDrinkPage>();
            pageRegistrar.Register<SettingsPageViewModel, SettingsPage>();
			pageRegistrar.Register<EditIngredientPageViewModel, EditIngredientPage>();
			pageRegistrar.Register<EditDrinkRecipePageViewModel, EditDrinkRecipePage>();

			var mainPage = pageRegistrar.Resolve<WelcomePageViewModel>();
          
            _app.MainPage = new NavigationPage(mainPage);

        }

    }
}
