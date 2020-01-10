using MeasurementEquipment.Scales;
using MeasurementEquipment.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace AgCROSScaleApp
{
    public class Constants
    {
        public const string TestDevicePortName = "TestPort";
        public static readonly SerialPortValue NullCOM = new SerialPortValue
        {
            DisplayName = "No Ports Active",
            PortName = "COM0"
        };
    }

    public class ScaleAppViewModel
    {
        public List<ScaleReadingValue> Readings { get; set; }

        public List<SerialPortValue> SerialPorts { get; set; }

        public SerialPortValue SelectedSerialPort { get; set; }

        public IScale Device { get; internal set; }

        public bool AllowTestDevice { get; set; }

        public string SaveFileName { get; set; }

        public int ConnectTimeout { get; set; }

        public ScaleAppViewModel()
        {
            var rand = new Random();
            this.AllowTestDevice = false;
            ReadUserSettings();
        }

        private void ReadUserSettings()
        {
            this.SaveFileName = Properties.Settings.Default.SaveFileName;
            this.AllowTestDevice = Properties.Settings.Default.AllowTestDevice;
            this.SelectedSerialPort = Properties.Settings.Default.LastPortSelected;
            this.ConnectTimeout = Properties.Settings.Default.ConnectTimeout;
            if (this.ConnectTimeout < 10) this.ConnectTimeout = 10;
            //ReadSavedFile();
        }

        internal void SaveState()
        {
            Properties.Settings.Default.SaveFileName = this.SaveFileName;
            Properties.Settings.Default.AllowTestDevice = this.AllowTestDevice;
            Properties.Settings.Default.LastPortSelected = this.SelectedSerialPort;
            Properties.Settings.Default.ConnectTimeout = this.ConnectTimeout;
            Properties.Settings.Default.Save();
            SaveFile();
        }

        public void DetectSerialPorts()
        {
            string query = String.Format("select * from Win32_SerialPort");
            var serialPorts = new List<SerialPortValue>();
            using (var searcher = new ManagementObjectSearcher(query))
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
                        serialPorts.Add(new SerialPortValue
                        {
                            DisplayName = result.GetPropertyValue("Name").ToString(),
                            PortName = result.GetPropertyValue("DeviceID").ToString()

                        });
                    }
                }
            }
            if (AllowTestDevice)
            {
                serialPorts.Add(new SerialPortValue { DisplayName = "Test Device", PortName = Constants.TestDevicePortName });
            }
            if (serialPorts.Count == 0)
            {
                serialPorts.Add(Constants.NullCOM);
            }
            this.SerialPorts = serialPorts;
        }

        public bool ConnectToDevice(SerialPortValue selectedItem)
        {
            if (Constants.NullCOM.Equals(selectedItem))
                throw new InvalidOperationException("No devices to connect to...");
            this.SelectedSerialPort = selectedItem;
            if (selectedItem.PortName.Equals(Constants.TestDevicePortName))
            {
                this.Device = new TestScaleDevice();
            }
            else
            {
                this.Device = new MS104TSSerialCom(
                    new MeasurementEquipment.Configurations.SerialConfiguration()
                    {
                        PortName = selectedItem.PortName,
                        BaudRate = 9600,
                        DataBits = 8,
                        Handshake = System.IO.Ports.Handshake.None,
                        Parity = System.IO.Ports.Parity.Even,
                        StopBits = System.IO.Ports.StopBits.One
                    },
                    TimeSpan.FromSeconds(60));
            }
            this.Device.Connect();
            if (this.Device.IsConnected)
            {
                if (this.Readings == null)
                {
                    this.Readings = new List<ScaleReadingValue>();
                }
            }
            return this.Device.IsConnected;
        }

        public void ReadSavedFile()
        {
            if (string.IsNullOrWhiteSpace(this.SaveFileName))
            {
                return;
            }

            if (!File.Exists(this.SaveFileName))
            {
                return;
            }
            Readings = new List<ScaleReadingValue>();
            using (var streamReader = new StreamReader(this.SaveFileName))
            {
                var header = streamReader.ReadLine(); // dumping
                while (!streamReader.EndOfStream)
                {
                    var line = streamReader.ReadLine().Split(',');
                    if (line.Length == 4)
                    {
                        Readings.Add(new ScaleReadingValue
                        {
                            RowID = int.Parse(line[0]),
                            ID = line[1],
                            ReadingTimeStamp = DateTime.Parse(line[2]),
                            ReadingValue = double.Parse(line[3]),
                            RepeatMeasurement = false
                        });
                    }
                }

            }
        }

        public ScaleReadingValue TakeReading(int rowId, string idTxt)
        {
            var timestamp = DateTime.Now;
            var reading = this.ReadWeight();
            ScaleReadingValue srValue;
            if (this.Readings.ElementAtOrDefault(rowId) != null)
            {
                this.Readings[rowId].ReadingValue = reading;
                this.Readings[rowId].ReadingTimeStamp = timestamp;
                srValue = this.Readings[rowId];
            }
            else
            {
                srValue = new ScaleReadingValue
                {
                    RowID = rowId,
                    ReadingTimeStamp = timestamp,
                    ReadingValue = reading,
                    ID = idTxt
                };
                this.Readings.Add(srValue);
            }
            return srValue;
        }

        public double ReadWeight()
        {
            return this.Device.TakeStableReading();
        }

        public void SaveFile()
        {
            if (!string.IsNullOrWhiteSpace(this.SaveFileName) && (Readings != null ? Readings.Count() > 0 : false))
            {
                using (var streamwriter = new StreamWriter(this.SaveFileName))
                {
                    streamwriter.WriteLine("rownumber,recordid,timestamp,reading");
                    foreach (var record in Readings)
                    {
                        streamwriter.WriteLine($"{record.RowID},{record.ID},{record.ReadingTimeStamp.ToString("o")},{record.ReadingValue}");
                    }
                }
            }
        }

        internal string UpdateFileSave(string fileName)
        {
            this.SaveFileName = fileName;
            if (this.Readings == null)
            {
                this.Readings = new List<ScaleReadingValue>();
            }
            this.Readings.Clear();
            return this.SaveFileName;
        }
    }
}