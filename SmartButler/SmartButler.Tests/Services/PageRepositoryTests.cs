using SmartButler.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using NUnit.Framework;
using SmartButler.Bootstrapper;
using SmartButler.Services.Registrable;
using SmartButler.ViewModels;
using SmartButler.Views;
using Android.Bluetooth;
using SmartButler.Bootstrapper.Modules;

namespace SmartButler.Tests.Services
{
    class PageRepositoryTests
    {
        private IPageRegistrar _pageRegistrar;

        [SetUp()]
        public void Setup()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<PageComponentsModule>();
            builder.RegisterType<PageRegistrar>().As<IPageRegistrar>().SingleInstance();

            var container = builder.Build();

            _pageRegistrar = container.Resolve<IPageRegistrar>();
        }

        [Test]
        public void Register_SameViewTwice_ThrowsException()
        {
            // arrange
            _pageRegistrar.Register<BluetoothPage, BluetoothPageViewModel>();

            // act // assert
            Assert.Throws<DuplicateViewRegisteredException>(() => 
                _pageRegistrar.Register<BluetoothPage, BluetoothPageViewModel>());

        }

        [Test]
        public void Resolve_View_ViewIsResolved()
        {
            // arrange
            _pageRegistrar.Register<BluetoothPage, BluetoothPageViewModel>();

            // act
            var view = _pageRegistrar.Resolve<BluetoothPage>();

            // assert
            Assert.That(view is BluetoothPage v && v.BindingContext is BluetoothPageViewModel);
        }


    }
}
