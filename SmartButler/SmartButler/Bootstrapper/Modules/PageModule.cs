using Autofac;
using SmartButler.ViewModels;
using SmartButler.Views;
using SmartButler.Views.Registerable;

namespace SmartButler.Bootstrapper.Modules
{
    public class PageModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            // add pages and ViewModels
            builder.RegisterType<WelcomePage>();
            builder.RegisterType<WelcomePageViewModel>().OnActivating(c =>
            {
                var dep = c.Context.Resolve<ToolbarControlViewModel>();
                c.Instance.SetToolBarControlViewModel(dep);
            });

            builder.RegisterType<BluetoothPage>();
            builder.RegisterType<BluetoothPageViewModel>().PropertiesAutowired().OnActivating(c =>
            {
                var dep = c.Context.Resolve<ToolbarControlViewModel>();
                c.Instance.SetToolBarControlViewModel(dep);
            });

            builder.RegisterType<DrinksPage>();
            builder.RegisterType<DrinksPageViewModel>().PropertiesAutowired().OnActivating(c =>
            {
                var dep = c.Context.Resolve<ToolbarControlViewModel>();
                c.Instance.SetToolBarControlViewModel(dep);
            });

            builder.RegisterType<BottlesPage>();
            builder.RegisterType<BottlesPageViewModel>().PropertiesAutowired().OnActivating(c =>
            {
                var dep = c.Context.Resolve<ToolbarControlViewModel>();
                c.Instance.SetToolBarControlViewModel(dep);
            });

            builder.RegisterType<MakeDrinkPage>();
            builder.RegisterType<MakeDrinkPageViewModel>().PropertiesAutowired().OnActivating(c =>
            {
                var dep = c.Context.Resolve<ToolbarControlViewModel>();
                c.Instance.SetToolBarControlViewModel(dep);
            });

            builder.RegisterType<SettingsPage>();
            builder.RegisterType<SettingsPageViewModel>();



        }
    }
}
