﻿using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;
using SmartButler.Framework.Bluetooth;
using SmartButler.Logic.Common;
using SmartButler.Logic.Interfaces;
using SmartButler.Logic.ViewModels.BaseViewModels;

namespace SmartButler.Logic.ViewModels
{

    public class BluetoothPageViewModel : ToolBarPageViewModelBase
    {
	    public ReactiveList<BluetoothDevice> BluetoothDevices { get; set; } = new ReactiveList<BluetoothDevice>();

        private readonly IBluetoothService _bluetoothService;
        private readonly INavigationService _navigationService;
        private readonly IUserInteraction _userInteraction;
        

        public BluetoothPageViewModel(IBluetoothService bluetoothService, INavigationService navigationService, IUserInteraction userInteraction) 
        {
            _bluetoothService = bluetoothService;
            _navigationService = navigationService;
            _userInteraction = userInteraction;

			SendCommand = ReactiveCommand.Create(async () => await _bluetoothService.WriteAsync("Test"));
		}

        public ReactiveCommand SendCommand { get; }

        public async Task DeviceSelectedAsync(string mac, string name)
        {
            IsBusy = true;

            var connected = await _bluetoothService.ConnectAsync(name, mac);
            if (connected)
            { 
                await _userInteraction.DisplayAlertAsync("Info", "Connected to device!", "OK");
                await _navigationService.PopToRootAsync();
            }
            else
            {
                await _userInteraction.DisplayAlertAsync("Info", "You were not able to connect to the device!", "OK");
            }

            IsBusy = false;
        }

        public void ConfigureViewModel()
        {
	        var devices = _bluetoothService.GetBondedDevices();
            foreach (var device in devices)
            {
                if (!BluetoothDevices.Any(d => d.Name == device.Name && d.Mac == device.Mac))
                    BluetoothDevices.Add(new BluetoothDevice(device.Name, device.Mac));
            }

        }

    }
}
