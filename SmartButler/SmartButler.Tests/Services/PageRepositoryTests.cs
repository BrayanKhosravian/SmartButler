using SmartButler.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using NUnit.Framework;
using SmartButler.Bootstrapper;
using SmartButler.ViewModels;
using SmartButler.Views;

namespace SmartButler.Tests.Services
{
    class PageRepositoryTests
    {
        private IPageRepository _pageRepository;

        [SetUp()]
        public void Setup()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<PageModule>();
            builder.RegisterType<PageRepository>().As<IPageRepository>().SingleInstance();

            var container = builder.Build();

            _pageRepository = container.Resolve<IPageRepository>();
        }

        [Test]
        public void Register_SameViewTwice_ThrowsException()
        {
            // arrange
            _pageRepository.Register<BluetoothDevicesListView, BluetoothDevicesViewModel>();

            // act // assert
            Assert.Throws<DuplicateViewRegisteredException>(() => 
                _pageRepository.Register<BluetoothDevicesListView, BluetoothDevicesViewModel>());

        }

        [Test]
        public void Resolve_View_ViewIsResolved()
        {
            // arrange
            _pageRepository.Register<BluetoothDevicesListView, BluetoothDevicesViewModel>();

            // act
            var view = _pageRepository.Resolve<BluetoothDevicesListView>();

            // assert
            Assert.That(view is BluetoothDevicesListView v && v.BindingContext is BluetoothDevicesViewModel);
        }
    }
}
