using Autofac;
using SmartButler.Logic.ViewModels;
using SmartButler.View.Pages;

namespace SmartButler.Bootstrapper.Modules
{
    public class PageComponentsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            // register control-viewmodels
            builder.RegisterType<ToolbarControlViewModel>()
	            .SingleInstance(); // single instance because settings should be saved

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
            builder.RegisterType<AddIngredientPageViewModel>();

            builder.RegisterType<ConfigureDrinkRecipePage>();
            builder.RegisterType<EditDrinkRecipePageViewModel>();
            builder.RegisterType<AddDrinkRecipePageViewModel>();

            builder.RegisterType<SettingsPage>();
            builder.RegisterType<SettingsPageViewModel>();

        }
    }
}
