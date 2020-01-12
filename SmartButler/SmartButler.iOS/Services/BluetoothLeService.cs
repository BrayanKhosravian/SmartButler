using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AddressBookUI;
using Foundation;
using UIKit;
using CoreBluetooth;
using iAd;
using SmartButler.Framework.Bluetooth;

// https://developer.apple.com/documentation/corebluetooth
// https://www.freecodecamp.org/news/ultimate-how-to-bluetooth-swift-with-hardware-in-20-minutes/
// https://developer.apple.com/library/archive/documentation/NetworkingInternetWeb/Conceptual/CoreBluetooth_concepts/AboutCoreBluetooth/Introduction.html#//apple_ref/doc/uid/TP40013257

namespace SmartButler.iOS.Services
{
	public class BluetoothLeService : IBluetoothService
	{
		private readonly CBCentralManager _manager = new CBCentralManager();
		private CBPeripheral _peripheral;

		private IList<BluetoothDevice> _devices = new List<BluetoothDevice>();
		private IList<CBPeripheral> _nativeDevices = new List<CBPeripheral>();

		public BluetoothLeService()
		{
			_manager.UpdatedState += ManagerOnUpdatedState;		// occurs when bt state changes // turing on or off

			_manager.ConnectionEventDidOccur += ManagerOnConnectionEventDidOccur;
			_manager.DisconnectedPeripheral += ManagerOnDisconnectedPeripheral;
			_manager.FailedToConnectPeripheral += ManagerOnFailedToConnectPeripheral;

			_manager.ConnectedPeripheral += ManagerOnConnectedPeripheral;
			_manager.DiscoveredPeripheral += ManagerOnDiscoveredPeripheral;
			_manager.RetrievedConnectedPeripherals += ManagerOnRetrievedConnectedPeripherals;
			_manager.RetrievedPeripherals += ManagerOnRetrievedPeripherals;
		}

		#region Handlers

		private void ManagerOnRetrievedPeripherals(object sender, CBPeripheralsEventArgs e)
		{
			
		}

		private void ManagerOnRetrievedConnectedPeripherals(object sender, CBPeripheralsEventArgs e)
		{
			_devices = new List<BluetoothDevice>();

			foreach (var peripheral in e.Peripherals)
			{
				_devices.Add(new BluetoothDevice(peripheral.Name, peripheral.Identifier.AsString()));
				_nativeDevices.Add(peripheral);
			}

		}

		private void ManagerOnDiscoveredPeripheral(object sender, CBDiscoveredPeripheralEventArgs e)
		{
			_devices.Add(new BluetoothDevice(e.Peripheral.Name, e.Peripheral.Identifier.AsString()));
		}

		private void ManagerOnConnectionEventDidOccur(object sender, CBPeripheralConnectionEventEventArgs e)
		{
			_peripheral = e.Peripheral;
		}

		private void ManagerOnDisconnectedPeripheral(object sender, CBPeripheralErrorEventArgs e)
		{
			
		}

		private void ManagerOnUpdatedState(object sender, EventArgs e)
		{
			
		}

		// connection

		private void ManagerOnFailedToConnectPeripheral(object sender, CBPeripheralErrorEventArgs e)
		{
			
		}

		private void ManagerOnConnectedPeripheral(object sender, CBPeripheralEventArgs e)
		{
			_peripheral = e.Peripheral;
		}


		#endregion

		public bool Enable()
		{
			return false;
		}

		public void Disable()
		{
			
		}

		public bool IsConnected() => _peripheral != null && _peripheral.IsConnected;

		public Task<bool> ConnectAsync(string name, string mac)
		{
			foreach (var device in _nativeDevices)
			{
				if (name != device.Name || mac != device.Identifier.AsString()) continue;

				try
				{
					_manager.ConnectPeripheral(device);
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
					return Task.FromResult(false);
				}
			}

			return Task.FromResult(false);
		}

		public Task<bool> WriteAsync(string msg)
		{
			try
			{
				_peripheral.WriteValue(NSData.FromString(msg), new CBMutableDescriptor(_peripheral.UUID, _peripheral.Self));
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return Task.FromResult(false);
			}

			return Task.FromResult(true);
		}

		public Task<bool> WriteAsync(byte[] buffer, int offset, int count)
		{
			try
			{
				_peripheral.WriteValue(NSData.FromArray(buffer), new CBMutableDescriptor(_peripheral.UUID, _peripheral.Self));
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return Task.FromResult(false);
			}

			return Task.FromResult(true);
		}

		public Task<BluetoothData> ReadAsync(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<BluetoothDevice> GetBondedDevices()
		{
			_manager.RetrievePeripheralsWithIdentifiers();
			return _devices;
		}

		public IEnumerable<BluetoothDevice> ScanForDevices()
		{
			_devices = new List<BluetoothDevice>();
			_nativeDevices = new List<CBPeripheral>();
			_manager?.ScanForPeripherals(CBUUID.FromString("00001101-0000-1000-8000-00805F9B34FB"));
			_manager?.StopScan();

			return _devices;


		}
	}
}