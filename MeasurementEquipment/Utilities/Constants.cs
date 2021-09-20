using MeasurementEquipment.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeasurementEquipment.Utilities
{
    public class Constants
    {
        public const string TestDevicePortName = "TestPort";
        public static readonly SerialPortValue NullCOM = new SerialPortValue
        {
            DisplayName = "No Ports Active",
            PortName = "COM0"
        };


        public enum ScaleUnits
        {
            g = 0,
            kg = 1,
            mg = 3,
            ug = 4
        }

    }
}
