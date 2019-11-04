namespace SmartButler.Framework.Bluetooth
{
    public class BluetoothDevice
    {
        public string Name { get; set; }
        public string Mac { get; set; }

        public BluetoothDevice()
        {
            
        }

        public BluetoothDevice(string name, string mac)
        {
            Name = name;
            Mac = mac;
        }
    }
}
