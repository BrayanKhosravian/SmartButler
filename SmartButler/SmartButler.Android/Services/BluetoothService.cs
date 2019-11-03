using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Android.Bluetooth;
using Java.Util;
using SmartButler.Core;
using SmartButler.Interfaces;
using Exception = System.Exception;

namespace SmartButler.Droid.Services
{

	class BluetoothService : IBluetoothService
    {
       // public event EventHandler<BluetoothEventArgs> CallbackReceived;

        private BluetoothAdapter _bluetoothAdapter = BluetoothAdapter.DefaultAdapter;
        private static BluetoothSocket _socket = null;
        private static Stream _outputStream = null;
        private static Stream _inputStream = null;

        private static string _connectedMac = string.Empty;

        public BluetoothService()
        {
            Enable();
        }

        public bool IsConnected()
        {
            if (_socket != null && _inputStream != null && _outputStream != null)
                return _socket.IsConnected;

            return false;
        }


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

        public void Disable() => _bluetoothAdapter?.Disable();


        // default uuid = "00001101-0000-1000-8000-00805F9B34FB"
        public async Task<bool> ConnectAsync(string name, string mac)
        {
            if (_socket != null && _socket.IsConnected && _connectedMac == mac)
            {
                if ( _socket.IsConnected && _inputStream != null && _outputStream != null)
                    return true;

                Close();
                return false;
            }

            _connectedMac = mac;

            Enable();
            var device = _bluetoothAdapter.BondedDevices.FirstOrDefault(d => d.Name == name && d.Address == mac);
            var uuids = device?.GetUuids();

            if (device == null) 
                return false;

            try
            {
                _socket = device.CreateRfcommSocketToServiceRecord(uuids.First().Uuid);

                // await _socket.ConnectAsync();
                
                await RetryHelper.RetryOnExceptionAsync<Java.IO.IOException>(2, TimeSpan.FromSeconds(5), 
                    async () => await _socket.ConnectAsync());

                _inputStream = _socket.InputStream;
                _outputStream = _socket.OutputStream;
            }
            catch (Java.IO.IOException e)
            {
                Console.WriteLine(e);
                Close();
                return false;
            }

            return IsConnected();

        }

        public Task<bool> WriteAsync(string msg)
        {
            if (!IsConnected())
                return Task.FromResult(false);

            Enable();

            int offset = 0;
            byte[] buffer = new Java.Lang.String(msg).GetBytes();

            return WriteAsync(buffer, offset, buffer.Length);

        }

        public async Task<bool> WriteAsync(byte[] buffer, int offset, int count)
        {
            if (!IsConnected())
                return false;

            Enable();

            try
            {
                await _outputStream.WriteAsync(buffer, offset, buffer.Length);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public async Task<BluetoothData> ReadAsync(byte[] buffer, int offset, int count)
        {
            Enable();

            try
            {
                int data = await _inputStream.ReadAsync(buffer, offset, count);
                var result = new BluetoothData(true, data);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Close();
                return new BluetoothData(false);

            }
        }


        public IEnumerable<Core.BluetoothDevice> GetBondedDevices()
        {
            Enable();
            
            foreach (var device in _bluetoothAdapter.BondedDevices)
                yield return new SmartButler.Core.BluetoothDevice(device.Name, device.Address);
            
        }



        public void Close()
        {
            _socket?.Close();
            _inputStream?.Close();
            _outputStream?.Close();
        }

    }
}