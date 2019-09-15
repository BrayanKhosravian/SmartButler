using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;
using SmartButler.Core;


namespace SmartButler.ViewModels
{
    public class BluetoothDevicesViewModel : BaseViewModel
    {

        public ReactiveList<BluetoothDevice> BluetoothDevices { get; private set; } = new ReactiveList<BluetoothDevice>();

    }
}
