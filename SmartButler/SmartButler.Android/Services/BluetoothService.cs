using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Util;
using SmartButler.Interfaces;
using SmartButler.Services;
using BluetoothDevice = SmartButler.Core.BluetoothDevice;
using Exception = System.Exception;
using IDisposable = System.IDisposable;

namespace SmartButler.Droid.Services
{

    class BluetoothService : IBluetoothService
    {
        public event EventHandler<BluetoothEventArgs> CallbackReceived;

        private BluetoothAdapter _bluetoothAdapter = BluetoothAdapter.DefaultAdapter;
        private Stream _outputStream;
        private Stream _inputStream;
        private string _connectedMac;
       

        public bool Enable()
        {
            // bluetooth not supported for device
            if (_bluetoothAdapter == null)
                return false;

            if (!_bluetoothAdapter.IsEnabled)
            {
                _bluetoothAdapter.Enable();
                return true;
            }

            return _bluetoothAdapter.IsEnabled;
        }

        public void Disable()
        {
            _bluetoothAdapter.Disable();
        }


        // default uuid = "00001101-0000-1000-8000-00805F9B34FB"
        public async Task<bool> ConnectAsync(string name, string mac)
        {
            _connectedMac = mac;
            BluetoothSocket socket;

            Enable();
            var device = _bluetoothAdapter.BondedDevices.FirstOrDefault(d => d.Name == name && d.Address == mac);

            var uuids = device?.GetUuids();

            if (device == null)
                return false;

            try
            {
                socket = device.CreateRfcommSocketToServiceRecord(UUID.FromString("00001101-0000-1000-8000-00805F9B34FB"));
                
                await socket.ConnectAsync();
                _inputStream = socket.InputStream;
                _outputStream = socket.OutputStream;

            }
            catch (Java.IO.IOException e)
            {
                Console.WriteLine(e);
                throw;
            }

            return socket.IsConnected;

        }

        public Task WriteAsync(string msg)
        {
            Enable();

            int offset = 0;
            byte[] buffer = new Java.Lang.String(msg).GetBytes();

            return WriteAsync(buffer, offset, buffer.Length);
        }

        public Task WriteAsync(byte[] buffer, int offset, int count)
        {
            Enable();

            try
            {
                return _outputStream.WriteAsync(buffer, offset, buffer.Length);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public Task<int> ReadAsync(byte[] buffer, int offset, int count)
        {
            Enable();

            try
            {
                return _inputStream.ReadAsync(buffer, offset, count);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        public IEnumerable<BluetoothDevice> GetBondedDevices()
        {
            Enable();
            
            foreach (var device in _bluetoothAdapter.BondedDevices)
                yield return new Core.BluetoothDevice(device.Name, device.Address);
            
        }

      

    }
}