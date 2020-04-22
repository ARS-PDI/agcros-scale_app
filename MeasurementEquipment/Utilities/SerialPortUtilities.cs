using MeasurementEquipment.Types;
using System;
using System.Collections.Generic;
using System.Management;
using System.Text;

namespace MeasurementEquipment.Utilities
{
    public class SerialPortUtilities
    {
        public static List<SerialPortValue> DetectSerialPorts(bool allowTest)
        {
            var serialPorts = new List<SerialPortValue>();
            using (var searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PnPEntity WHERE Caption like '%(COM%'"))
            using (var results = searcher.Get())
            {
                foreach (ManagementObject result in results)
                {
                    using (result)
                    {
                        var displayName = result.GetPropertyValue("Name").ToString();
                        if (displayName.Contains("Intel"))
                        {
                            continue;
                        }
                        var port = displayName.Split('(', ')')[1];
                        serialPorts.Add(new SerialPortValue
                        {
                            DisplayName = displayName,
                            PortName = port
                        });
                    }
                }
            }
            if (allowTest)
            {
                serialPorts.Add(new SerialPortValue { DisplayName = "Test Device", PortName = Constants.TestDevicePortName });
            }
            return serialPorts;
        }
    }
}
