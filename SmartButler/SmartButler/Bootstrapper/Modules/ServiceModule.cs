using Autofac;
using SmartButler.Repositories;
using SmartButler.Services.RegisterAble;
using SmartButler.Services.Registrable;
using Xamarin.Forms;

namespace SmartButler.Bootstrapper.Modules
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            // register services
            builder.RegisterType<UserInteraction>().As<IUserInteraction>();
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
