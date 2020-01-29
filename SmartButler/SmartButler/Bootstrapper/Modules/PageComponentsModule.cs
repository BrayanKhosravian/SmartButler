using System.Collections.Generic;
using Autofac;
using SmartButler.DataAccess.Repositories;
using SmartButler.Logic.Interfaces;
using SmartButler.Logic.ModelViewModels;
using SmartButler.Logic.Services;
using SmartButler.Logic.ViewModels;
using SmartButler.View.Pages;
using Xamarin.Forms;

namespace SmartButler.Bootstrapper.Modules
{
    public class PageComponentsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            // register control-viewmodels
            builder.RegisterType<ToolbarControlViewModel>().SingleInstance(); // single instance because settings should be saved

            // add pages and ViewModels
            builder.RegisterType<WelcomePage>();
            builder.RegisterType<WelcomePageViewModel>();

            builder.RegisterType<BluetoothPage>();
            builder.RegisterType<BluetoothPageViewModel>();

            builder.RegisterType<DrinksPage>();
            builder.RegisterType<DrinksPageViewModel>();

            builder.RegisterType<IngredientsPage>();
            builder.RegisterType<ShowIngredientsPageViewModel>();
            builder.RegisterType<SelectIngredientsPageViewModel>();

            builder.RegisterType<MakeDrinkPage>();
            builder.RegisterType<MakeDrinkPageViewModel>();

	        builder.RegisterType<ConfigureIngredientPage>();
            builder.RegisterType<EditIngredientPageViewModel>();

            builder.RegisterType<EditDrinkRecipePage>();
            builder.RegisterType<EditDrinkRecipePageViewModel>();

            builder.RegisterType<SettingsPage>();
            builder.RegisterType<SettingsPageViewModel>();

        }
    }
}
