using Autofac;
using SmartButler.Bootstrapper.Common;
using SmartButler.DataAccess.Repositories;
using SmartButler.Logic.Interfaces;
using SmartButler.Logic.Services;
using Xamarin.Forms;

namespace SmartButler.Bootstrapper.Modules
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            // register services
            builder.RegisterType<IngredientFactory>().As<IIngredientFactory>();
            builder.RegisterType<DrinkRecipeBuilder>().As<IDrinkRecipeBuilder>();
            builder.RegisterType<IngredientBuilder>().As<IIngredientBuilder>();

            // register singleton services
            builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
            builder.RegisterType<PageRegistrar>().As<IPageRegistrar>().SingleInstance();
            builder.RegisterType<UserInteraction>().As<IUserInteraction>().SingleInstance();
            builder.RegisterType<IngredientRepository>().As<IIngredientRepository>().SingleInstance();

            // register services lazily
            builder.Register(componentContext => ((App)Application.Current).MainPage.Navigation);


        }
    }
}
