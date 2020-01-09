using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;

namespace MeasurementEquipment.Configurations
{
    public class SerialConfiguration
    {
        public string PortName { get; set; }
        public int BaudRate { get; set; }
        public Parity Parity { get; set; }
        public StopBits StopBits { get; set; }
        public Handshake Handshake { get; set; }
        public int DataBits { get; set; }
    }
}
