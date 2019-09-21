using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Android.Bluetooth;
using Java.Util;
using SmartButler.Core;
using SmartButler.Interfaces;
using Exception = System.Exception;
using IDisposable = System.IDisposable;

namespace SmartButler.Droid.Services
{

    internal enum ConnectionState
    {
        Null,
        Disconnected,
        Connected
    }

    class BluetoothService : IBluetoothService
    {
        public event EventHandler<BluetoothEventArgs> CallbackReceived;

        private BluetoothAdapter _bluetoothAdapter = BluetoothAdapter.DefaultAdapter;
        private Stream _outputStream = null;
        private Stream _inputStream = null;

        private static string _connectedMac = string.Empty;
        private static ConnectionState _connectionState = ConnectionState.Null;


        public BluetoothService()
        {
            Enable();
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

        public void Disable()
        {
            _bluetoothAdapter.Disable();
        }


        // default uuid = "00001101-0000-1000-8000-00805F9B34FB"
        public async Task<bool> ConnectAsync(string name, string mac)
        {
            if (_connectedMac == mac)
            {
                if (_connectionState == ConnectionState.Connected)
                    return true;
                return false;
            }

            _connectedMac = mac;

            BluetoothSocket socket = null;

            Enable();
            var device = _bluetoothAdapter.BondedDevices.FirstOrDefault(d => d.Name == name && d.Address == mac);

            var uuids = device?.GetUuids();

            if (device == null)
            {
                _connectionState = ConnectionState.Disconnected;
                return false;
            }

            try
            {
                socket = device.CreateRfcommSocketToServiceRecord(UUID.FromString("00001101-0000-1000-8000-00805F9B34FB"));
                
                await socket.ConnectAsync();
                _inputStream = socket.InputStream;
                _outputStream = socket.OutputStream;
                _connectionState = ConnectionState.Connected;

            }
            catch (Java.IO.IOException e)
            {
                Console.WriteLine(e);
                _inputStream?.Close();
                _outputStream?.Close();
                _connectionState = ConnectionState.Disconnected;
                return false;
            }


            if (socket == null || _inputStream == null || _outputStream == null)
            {
                _connectionState = ConnectionState.Disconnected;
                return false;
            }

            if (socket.IsConnected)
            {
                _connectionState = ConnectionState.Connected;
                return true;
            }
            else
            {
                _connectionState = ConnectionState.Disconnected;
                return false;
            }

        }

        public async Task<bool> WriteAsync(string msg)
        {
            if (_connectionState == ConnectionState.Disconnected)
                return false;
            Enable();

            int offset = 0;
            byte[] buffer = new Java.Lang.String(msg).GetBytes();

            return await WriteAsync(buffer, offset, buffer.Length);

        }

        public async Task<bool> WriteAsync(byte[] buffer, int offset, int count)
        {
            if (_connectionState == ConnectionState.Disconnected)
                return false;

            Enable();

            try
            {
                await _outputStream.WriteAsync(buffer, offset, buffer.Length);
                _connectionState = ConnectionState.Connected;
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _connectionState = ConnectionState.Disconnected;
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
                _connectionState = ConnectionState.Connected;
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _connectionState = ConnectionState.Disconnected;
                return new BluetoothData(false);

            }
        }


        public IEnumerable<Core.BluetoothDevice> GetBondedDevices()
        {
            Enable();
            
            foreach (var device in _bluetoothAdapter.BondedDevices)
                yield return new SmartButler.Core.BluetoothDevice(device.Name, device.Address);
            
        }

    }


}