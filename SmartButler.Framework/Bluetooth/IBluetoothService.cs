using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartButler.Framework.Bluetooth
{


	public interface IBluetoothService
	{
		bool Enable();
		void Disable();
		bool IsConnected();
		Task<bool> ConnectAsync(string name, string mac);

		Task<bool> WriteAsync(string msg);
		Task<bool> WriteAsync(byte[] buffer, int offset, int count);
		Task<BluetoothData> ReadAsync(byte[] buffer, int offset, int count);

		IEnumerable<BluetoothDevice> GetBondedDevices();

	}
}
