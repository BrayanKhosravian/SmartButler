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
            builder.RegisterType<WelcomePageViewModel>().OnActivating(c =>
            {
                var dep = c.Context.Resolve<ToolbarControlViewModel>();
                c.Instance.SetToolBarControlViewModel(dep);
            });

            builder.RegisterType<BluetoothPage>();
            builder.RegisterType<BluetoothPageViewModel>().OnActivating(c =>
            {
                var dep = c.Context.Resolve<ToolbarControlViewModel>();
                c.Instance.SetToolBarControlViewModel(dep);
            });

            builder.RegisterType<DrinksPage>();
            builder.RegisterType<DrinksPageViewModel>().OnActivating(c =>
            {
                var dep = c.Context.Resolve<ToolbarControlViewModel>();
                c.Instance.SetToolBarControlViewModel(dep);
            });

            builder.RegisterType<IngredientsPage>();
            builder.RegisterType<ShowIngredientsPageViewModel>().OnActivating(c =>
            {
                var dep = c.Context.Resolve<ToolbarControlViewModel>();
                c.Instance.SetToolBarControlViewModel(dep);
            });
            builder.RegisterType<SelectIngredientsPageViewModel>().OnActivating(c =>
            {
	            var dep = c.Context.Resolve<ToolbarControlViewModel>();
	            c.Instance.SetToolBarControlViewModel(dep);
            });

            builder.RegisterType<MakeDrinkPage>();
            builder.RegisterType<MakeDrinkPageViewModel>().OnActivating(c =>
            {
                var dep = c.Context.Resolve<ToolbarControlViewModel>();
                c.Instance.SetToolBarControlViewModel(dep);
            });

	        builder.RegisterType<ConfigureIngredientPage>();
            builder.RegisterType<EditIngredientPageViewModel>().OnActivated(c =>
            {
	            var dep = c.Context.Resolve<ToolbarControlViewModel>();
	            c.Instance.SetToolBarControlViewModel(dep);

            });

            builder.RegisterType<EditDrinkRecipePage>();
            builder.RegisterType<EditDrinkRecipePageViewModel>().OnActivated(c =>
            {
	            var dep = c.Context.Resolve<ToolbarControlViewModel>();
	            c.Instance.SetToolBarControlViewModel(dep);

            });


            builder.RegisterType<SettingsPage>();
            builder.RegisterType<SettingsPageViewModel>();

        }
    }
}
