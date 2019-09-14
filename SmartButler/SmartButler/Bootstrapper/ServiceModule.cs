using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using SmartButler.Services;
using Xamarin.Forms;

namespace SmartButler.Bootstrapper
{
    class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<NavigationService>().As<INavigationService>();
            builder.RegisterType<PageRepository>().As<IPageRepository>();
            builder.RegisterType<ResourceManager>().As<IResourceManager>();
            builder.RegisterType<UserInteraction>().As<IUserInteraction>();
            builder.Register(componentContext => ((App)Application.Current).MainPage.Navigation);
        }
    }
}
