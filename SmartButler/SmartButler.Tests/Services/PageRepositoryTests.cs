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
            builder.RegisterModule<PageModule>();
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

        [Test]
        public void Resolve_TwoTimes_InstancesShouldNotEqual()
        {
            // arrange
            var builder = new ContainerBuilder();
            builder.RegisterType<ResourceManager>().As<IResourceManager>();
            var container = builder.Build();

            // act
            var instance1 = container.Resolve<IResourceManager>();
            var instance2 = container.Resolve<IResourceManager>();

            // assert
            Assert.That(!ReferenceEquals(instance1, instance2));
        }

        [Test]
        public void Resolve_TwoTimes_SingleInstance_InstancesShouldEqual()
        {
            // arrange
            var builder = new ContainerBuilder();
            builder.RegisterType<ResourceManager>().As<IResourceManager>().SingleInstance();
            var container = builder.Build();

            // act
            var instance1 = container.Resolve<IResourceManager>();
            var instance2 = container.Resolve<IResourceManager>();

            // assert
            Assert.That(ReferenceEquals(instance1, instance2));
        }

    }
}
