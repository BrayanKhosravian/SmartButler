using System;
using System.Collections.Generic;
using System.Text;

namespace SmartButler.Core
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
