using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Autofac;
using Moq;
using SmartButler.Framework.Bluetooth;
using SmartButler.Framework.Extensions;

namespace SmartButler.UWP
{
    public sealed partial class MainPage
    {
	    public MainPage()
        {
            this.InitializeComponent();
        }

	    public async Task ActivateAsync()
        {
            var app = new SmartButler.App();
            var builder = new ContainerBuilder();
            builder.RegisterInstance(new Mock<IBluetoothService>().Object).As<IBluetoothService>();

            await app.InjectPlatformDependenciesAsync(builder).AwaitSafe();

            LoadApplication(app);
        }
    }
}
