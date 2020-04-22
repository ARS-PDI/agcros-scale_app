using MeasurementEquipment.Scales;
using MeasurementEquipment.Types;
using MeasurementEquipment.Utilities;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgCROSScaleApp.Models
{
    [Serializable]
    public class ConnectionModel
    {
        private ILogger logger = Log.ForContext<ConnectionModel>();

        private IScale device;

        public int ConnectTimeout { get; set; }
        public int NumRetries { get; set; }
        public int BaudRate { get; set; }
        public int DataBits { get; set; }
        public System.IO.Ports.Handshake Handshake { get; set; } 
        public System.IO.Ports.Parity Parity { get; set; }
        public System.IO.Ports.StopBits StopBits { get; set; }
        public bool IsConnected => this.device?.IsConnected ?? false;

        public ConnectionModel()
        {
            ConnectTimeout = 5;
            NumRetries = 1;
            BaudRate = 9600;
            DataBits = 8;
            Handshake = System.IO.Ports.Handshake.None;
            Parity = System.IO.Ports.Parity.Even;
            StopBits = System.IO.Ports.StopBits.One;
        }

        public bool ConnectToDevice(SerialPortValue selectedItem)
        {
            if (selectedItem.PortName.Equals(Constants.TestDevicePortName))
            {
                this.device = new TestScaleDevice();
                logger.Debug("Set up test device");
            }
            else
            {
                this.device = new MTSICSSerialDevice(
                    new MeasurementEquipment.Configurations.SerialConfiguration()
                    {
                        PortName = selectedItem.PortName,
                        BaudRate = BaudRate,
                        DataBits = DataBits,
                        Handshake = Handshake,
                        Parity = Parity,
                        StopBits = StopBits
                    },
                    TimeSpan.FromSeconds(this.ConnectTimeout),
                    this.NumRetries);
                logger.Debug("Set up MS-SICS scale device.");
                logger.Debug("MS-SICS com settings: {name},Baud:9600,DataBits:8,Handshake:None,Parity:Even,StopBits:1",
                    selectedItem.PortName);

            }
            logger.Information("Attempting to connect to device of type {devicetype}", this.device.GetType());
            this.device.Connect();
            return this.device.IsConnected;
        }

        public void Disconnect()
        {
            if (IsConnected)
            {
                this.device.Disconnect();
            }
        }

        public double TakeStableReading()
        {
            return this.device.TakeStableReading();
        }


    }
}
