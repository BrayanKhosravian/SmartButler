using Autofac;
using SmartButler.Bootstrapper.Common;
using SmartButler.DataAccess.Repositories;
using SmartButler.Framework.Resources;
using SmartButler.Logic.Interfaces;
using SmartButler.Logic.ModelViewModels;
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
            builder.RegisterType<IngredientsFactory>().As<IIngredientFactory>();
            builder.RegisterType<DrinkRecipeBuilder>().As<IDrinkRecipeBuilder>();
            builder.RegisterType<IngredientBuilder>().As<IIngredientBuilder>();
            builder.RegisterType<UserInteraction>().As<IUserInteraction>();
            builder.RegisterType<CrossMediaService>().As<ICrossMediaService>();

            // register singleton services
			builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
            builder.RegisterType<PageRegistrar>().As<IPageRegistrar>().SingleInstance();
            builder.RegisterType<DrinkIngredientSelectionHost>()
	            .As<ISelectionHost<DrinkIngredientViewModel>>()
	            .SingleInstance();

            // register services lazily
            builder.Register(componentContext => ((App)Application.Current).MainPage.Navigation);


        }
    }
}
