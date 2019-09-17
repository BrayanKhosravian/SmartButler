using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using SmartButler.Services;
using SmartButler.Services.Registrable;
using Xamarin.Forms;

namespace SmartButler.Bootstrapper
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            // register services
            builder.RegisterType<ResourceManager>().As<IResourceManager>();
            builder.RegisterType<UserInteraction>().As<IUserInteraction>();

            // register singleton services
            builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
            builder.RegisterType<PageRepository>().As<IPageRepository>().SingleInstance();

            // register services lazily
            builder.Register(componentContext => ((App)Application.Current).MainPage.Navigation);


        }
    }
}
