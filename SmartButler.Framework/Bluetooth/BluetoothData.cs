namespace SmartButler.Framework.Bluetooth
{
    public class BluetoothData
    {
        public bool Result { get; }
        public int? Value { get; }

        public BluetoothData(bool result, int value)
        {
            Result = result;
            Value = value;
        }

        public BluetoothData(bool result)
        {
            Result = result;
        }
    }
}
