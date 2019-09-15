using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SmartButler.Interfaces;
using SmartButler.Services;
using BluetoothDevice = SmartButler.Core.BluetoothDevice;
using IDisposable = System.IDisposable;

namespace SmartButler.Droid.Services
{


    class BluetoothService : DisposableService, IBluetoothService
    {
        public event EventHandler<BluetoothEventArgs> CallbackReceived;

        private BluetoothAdapter _bluetoothAdapter = BluetoothAdapter.DefaultAdapter;
        private string _connectedMac;
        

        public BluetoothService()
        {
            
        }

        public void Enable()
        {

            if (!_bluetoothAdapter.IsEnabled)
                _bluetoothAdapter.Enable();
            
        }

        public void Disable()
        {
            if (_bluetoothAdapter.IsEnabled)
                _bluetoothAdapter.Disable();

            this.Dispose();
        }

        public bool Connect(string mac)
        {
            _connectedMac = mac;
            throw new NotImplementedException();
        }

    

        public void Send(string msg)
        {
            throw new NotImplementedException();
        }

       

        public IEnumerable<BluetoothDevice> GetBondedDevices()
        {
            Enable();
            
            foreach (var device in _bluetoothAdapter.BondedDevices)
                yield return new Core.BluetoothDevice(device.Name, device.Address);
            
        }

      

        public override void Dispose()
        {
            
        }
    }
}