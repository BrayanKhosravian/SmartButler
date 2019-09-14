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
            builder.RegisterType<BluetoothDevicesListView>();
            builder.RegisterType<BluetoothDevicesListView>();

            builder.RegisterType<DrinkSelectionListView>();
            builder.RegisterType<DrinkSelectionViewModel>();

            builder.RegisterType<BottlesListView>();
            builder.RegisterType<BottlesViewModel>();

            

            base.Load(builder);
        }
    }
}
