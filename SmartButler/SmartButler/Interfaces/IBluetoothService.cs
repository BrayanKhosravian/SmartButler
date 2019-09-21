using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SmartButler.Core;

namespace SmartButler.Interfaces
{
    public interface IBluetoothService
    {
        bool Enable();
        void Disable();
        Task<bool> ConnectAsync(string name, string mac);

        Task<bool> WriteAsync(string msg);
        Task<bool> WriteAsync(byte[] buffer, int offset, int count);
        Task<BluetoothData> ReadAsync(byte[] buffer, int offset, int count);

        IEnumerable<BluetoothDevice> GetBondedDevices();

        event EventHandler<BluetoothEventArgs> CallbackReceived;
    }

    public class BluetoothEventArgs : EventArgs
    {
        public string Mac { get; set; }
        public string Msg { get; set; }

        public BluetoothEventArgs()
        {
            
        }

        public BluetoothEventArgs(string mac, string msg)
        {
            Mac = mac;
            Msg = msg;
        }
      
    }
}
