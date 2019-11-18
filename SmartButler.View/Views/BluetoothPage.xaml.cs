using Xamarin.Forms;
using ReactiveUI;
using SmartButler.Framework.Bluetooth;
using SmartButler.Logic.ViewModels;
using Xamarin.Forms.Xaml;

namespace SmartButler.View.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BluetoothPage : ContentPage, IViewFor<BluetoothPageViewModel>
    {

        public BluetoothPage()
        {
            InitializeComponent();

            this.WhenActivated(disposer =>
            {
                ViewModel?.ConfigureViewModel();

            });


        }

		async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			if (e.Item == null)
				return;

			var device = ((ListView)sender).SelectedItem as BluetoothDevice;
			await ViewModel?.DeviceSelectedAsync(device.Mac, device.Name);
		}

		object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = value as BluetoothPageViewModel;
        }

        public BluetoothPageViewModel ViewModel
        {
            get => BindingContext as BluetoothPageViewModel;
            set => BindingContext = value;
        }
    }
}
