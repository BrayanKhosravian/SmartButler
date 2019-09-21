using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using SmartButler.ViewModels;
using SmartButler.Views;

namespace SmartButler.Bootstrapper
{
    public class PageModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<WelcomePage>();
            builder.RegisterType<WelcomePageViewModel>();

            builder.RegisterType<BluetoothPage>();
            builder.RegisterType<BluetoothPageViewModel>();

            builder.RegisterType<DrinksPage>();
            builder.RegisterType<DrinksPageViewModel>();

            builder.RegisterType<BottlesPage>();
            builder.RegisterType<BottlesPageViewModel>();

        }
    }
}
