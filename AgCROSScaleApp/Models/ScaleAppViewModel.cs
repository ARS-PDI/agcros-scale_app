using MeasurementEquipment.Scales;
using MeasurementEquipment.Types;
using Serilog;
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
        private ILogger logger;
        public List<ScaleReadingValue> Readings { get; set; }

        public List<SerialPortValue> SerialPorts { get; set; }

        public SerialPortValue SelectedSerialPort { get; set; }

        public IScale Device { get; internal set; }

        public bool AllowTestDevice { get; set; }

        public string SaveFileName { get; set; }

        public int ConnectTimeout { get; set; }

        private bool debug;

        public bool Debug
        {
            get
            {
                return debug;
            }

            set
            {
                if (value)
                    Program.LoggerSwitch.MinimumLevel = Serilog.Events.LogEventLevel.Debug;
                else
                    Program.LoggerSwitch.MinimumLevel = Serilog.Events.LogEventLevel.Warning;

                debug = value;
            }
        }

        public int NumRetries { get; set; }

        public ScaleAppViewModel()
        {
            var rand = new Random();
            this.AllowTestDevice = false;
            this.logger = Log.ForContext<ScaleAppViewModel>();
            ReadUserSettings();
        }

        private void ReadUserSettings()
        {
            logger.Debug("Reading User Settings...");
            this.SaveFileName = Properties.Settings.Default.SaveFileName;
            this.AllowTestDevice = Properties.Settings.Default.AllowTestDevice;
            this.SelectedSerialPort = Properties.Settings.Default.LastPortSelected;
            this.ConnectTimeout = Properties.Settings.Default.ConnectTimeout;
            this.Debug = Properties.Settings.Default.Debug;
            this.NumRetries = Properties.Settings.Default.NumRetries;
            if (this.Debug)
            {
                Program.LoggerSwitch.MinimumLevel = Serilog.Events.LogEventLevel.Debug;
            }
            logger.Debug(
                "Read User Settings: file:'{file}',testdevice:{testdev},serialport:{portnum},timeout:{tout},debug:{debug}",
                SaveFileName, AllowTestDevice, SelectedSerialPort?.PortName ?? "", ConnectTimeout, Debug);
            //ReadSavedFile();
        }

        internal void SaveState()
        {
            logger.Debug("Writing User Settings...");
            Properties.Settings.Default.SaveFileName = this.SaveFileName;
            Properties.Settings.Default.AllowTestDevice = this.AllowTestDevice;
            Properties.Settings.Default.LastPortSelected = this.SelectedSerialPort;
            Properties.Settings.Default.ConnectTimeout = this.ConnectTimeout;
            Properties.Settings.Default.Debug = this.Debug;
            Properties.Settings.Default.NumRetries = this.NumRetries;
            logger.Debug(
    "Saving User Settings: file:'{file}',testdevice:{testdev},serialport:{portnum},timeout:{tout},debug:{debug}",
    SaveFileName, AllowTestDevice, SelectedSerialPort?.PortName ?? "", ConnectTimeout, Debug);
            Properties.Settings.Default.Save();
            SaveFile();
        }

        public void DetectSerialPorts()
        {
            string query = String.Format("select * from Win32_SerialPort");
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
                logger.Debug("Set up test device");
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
                    TimeSpan.FromSeconds(this.ConnectTimeout),
                    this.NumRetries);
                logger.Debug("Set up MS-SICS scale device.");
                logger.Debug("MS-SICS com settings: {name},Baud:9600,DataBits:8,Handshake:None,Parity:Even,StopBits:1",
                    selectedItem.PortName);

            }
            logger.Information("Attempting to connect to device of type {devicetype}", this.Device.GetType());
            this.Device.Connect();
            if (this.Device.IsConnected)
            {
                if (this.Readings == null)
                {
                    logger.Debug("Reset readings.");
                    this.Readings = new List<ScaleReadingValue>();
                }
            }
            logger.Information("Connected to Device? {connected}", this.Device.IsConnected);
            return this.Device.IsConnected;
        }

        public void ReadSavedFile()
        {
            if (string.IsNullOrWhiteSpace(this.SaveFileName))
            {
                logger.Debug("Attempting to read in file: no file selected.");
                return;
            }

            if (!File.Exists(this.SaveFileName))
            {
                logger.Debug("Attempting to read in file: file does not exist.");
                return;
            }
            Readings = new List<ScaleReadingValue>();
            logger.Debug("Attempting to read in file, adding records as they are found...");
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
            logger.Debug("Attempting to take a reading...");

            var timestamp = DateTime.Now;
            var reading = this.ReadWeight();
            logger.Debug("Got a stable weight reading: {reading}", reading);
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
            logger.Debug("Attempting to save readings file...");

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
                logger.Debug("wrote to selected file.");
            }
        }

        internal string UpdateFileSave(string fileName)
        {
            logger.Debug("Updating Selected File...");

            this.SaveFileName = fileName;
            if (this.Readings == null)
            {
                this.Readings = new List<ScaleReadingValue>();
            }
            this.Readings.Clear();
            logger.Debug("File name updated, readings cleared");
            return this.SaveFileName;
        }
    }
}