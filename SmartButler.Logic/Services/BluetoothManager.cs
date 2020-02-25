using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using SmartButler.Framework.Bluetooth;

namespace SmartButler.Logic.Services
{
	//public interface IBluetoothManager
	//{

	//}

	//public class BluetoothManager : IBluetoothManager
	//{
	//	private readonly IBluetoothLE _bluetooth;
	//	public BluetoothManager(IBluetoothLE bluetooth)
	//	{
	//		_bluetooth = bluetooth;
	//		_bluetooth.Adapter.DeviceDiscovered += AdapterOnDeviceDiscovered;
	//	}

	//	private IList<IDevice> _discoveredDevices = new List<IDevice>();
	//	private IDevice _connectedDevice;

	//	public Subject<BluetoothDevice> DiscoveredDevices => new Subject<BluetoothDevice>();

	//	public Task StartScanning()
	//	{
	//		return _bluetooth.Adapter.StartScanningForDevicesAsync();
	//	}

	//	public Task ConnectAsync(BluetoothDevice btDevice)
	//	{
	//		_connectedDevice =
	//			_discoveredDevices.FirstOrDefault(d => d.Name == btDevice.Name && d.Id.ToString() == btDevice.Mac);

	//		try
	//		{
	//			return _bluetooth.Adapter.ConnectToDeviceAsync(_connectedDevice);
	//		}
	//		catch (Exception e)
	//		{
	//			Console.WriteLine(e);
	//			throw;
	//		}
	//	}

	//	private void AdapterOnDeviceDiscovered(object sender, DeviceEventArgs e)
	//	{
	//		DiscoveredDevices.OnNext(new BluetoothDevice(e.Device.Name, e.Device.Id.ToString()));
	//		if(!_discoveredDevices.Contains(e.Device))
	//			_discoveredDevices.Add(e.Device);

	//	}
	//}
}
