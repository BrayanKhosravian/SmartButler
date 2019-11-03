using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;
using SmartButler.Core;
using SmartButler.Interfaces;
using SmartButler.Services.RegisterAble;
using SmartButler.Services.Registrable;
using SmartButler.Views;
using Xamarin.Forms;


namespace SmartButler.ViewModels
{

    public class BluetoothPageViewModel : BaseViewModel
    {
        //public ObservableCollection<BluetoothDevice> BluetoothDevices  { get; private set; } = new ObservableCollection<BluetoothDevice>();
        public ReactiveList<BluetoothDevice> BluetoothDevices { get; set; } = new ReactiveList<BluetoothDevice>();

        private readonly IBluetoothService _bluetoothService;
        private readonly INavigationService _navigationService;
        private readonly IUserInteraction _userInteraction;
        

        public BluetoothPageViewModel(IBluetoothService bluetoothService, INavigationService navigationService, IUserInteraction userInteraction) 
        {
            _bluetoothService = bluetoothService;
            _navigationService = navigationService;
            _userInteraction = userInteraction;
        }

        public ICommand SendCommand => new Command(async () => await _bluetoothService.WriteAsync("Test"));

        public async Task DeviceSelectedAsync(string mac, string name)
        {
            IsBusy = true;

            var connected = await _bluetoothService.ConnectAsync(name, mac);
            if (connected)
            { 
                await _userInteraction.DisplayAlert("Info", "Connected to device!", "OK");
                await _navigationService.PopToRootAsync();
            }
            else
            {
                await _userInteraction.DisplayAlert("Info", "You were not able to connect to the device!", "OK");
            }

            IsBusy = false;
        }

        public void ConfigureViewModel()
        {
            //BluetoothDevices.Add(new BluetoothDevice("DummyName", "DummyMac"));

            var devices = _bluetoothService.GetBondedDevices();
            foreach (var device in devices)
            {
                if (!BluetoothDevices.Any(d => d.Mac == device.Mac && d.Name == device.Name))
                    BluetoothDevices.Add(device);
            }

        }

        public ToolbarControlViewModel ToolbarControlViewModel { get; private set; }
        public void SetToolBarControlViewModel(ToolbarControlViewModel vm)
        {
            ToolbarControlViewModel = vm;
        }

    }
}
