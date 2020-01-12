using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth;
using Windows.Networking.Sockets;
using SmartButler.Framework.Bluetooth;
using BluetoothDevice = SmartButler.Framework.Bluetooth.BluetoothDevice;

namespace SmartButler.UWP.Services
{
	class BluetoothServiceLe : IBluetoothService
	{
		//private readonly 
		//private BluetoothDevice _device;
		//private StreamSocket _socket;

		public bool Enable()
		{
			throw new NotImplementedException();
		}

		public void Disable()
		{
			throw new NotImplementedException();
		}

		public bool IsConnected()
		{
			throw new NotImplementedException();
		}

		public Task<bool> ConnectAsync(string name, string mac)
		{
			throw new NotImplementedException();
		}

		public Task<bool> WriteAsync(string msg)
		{
			throw new NotImplementedException();
		}

		public Task<bool> WriteAsync(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		public Task<BluetoothData> ReadAsync(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<BluetoothDevice> GetBondedDevices()
		{
			throw new NotImplementedException();
		}
	}
}
