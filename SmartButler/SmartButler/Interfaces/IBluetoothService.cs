using System;
using System.Collections.Generic;
using System.Text;
using SmartButler.Core;

namespace SmartButler.Interfaces
{
    public interface IBluetoothService : IDisposable
    {
        void On();
        void Off();
        void Send(string msg);
        bool Connect(string mac);

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
