using MeasurementEquipment.Scales;
using MeasurementEquipment.Types;
using MeasurementEquipment.Utilities;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AgCROSScaleApp.Models
{
    public class ScaleAppViewModel
    {
        public const double ZeroEpsilon = 0.00001;
        private ILogger logger;

        public FileSaveConfigurationModel FileSave { get; set; }

        public ConnectionModel Connection { get; set; }
        public List<ScaleReadingValue> Readings { get; set; }

        public List<SerialPortValue> SerialPorts { get; set; }

        public SerialPortValue SelectedSerialPort { get; set; }

        public bool UpdatedRecords { get; set; }

        public bool AllowTestDevice { get; set; }

        public bool PromptOnZero { get; set; }

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

        public ScaleAppViewModel()
        {
            var rand = new Random();
            this.AllowTestDevice = false;
            this.logger = Log.ForContext<ScaleAppViewModel>();
            ReadUserSettings();
            UpdatedRecords = false;
        }

        private void ReadUserSettings()
        {
            logger.Debug("Reading User Settings...");
            this.AllowTestDevice = Properties.Settings.Default.AllowTestDevice;
            this.SelectedSerialPort = Properties.Settings.Default.LastPortSelected;
            this.Debug = Properties.Settings.Default.Debug;
            this.FileSave = Properties.Settings.Default.ScaleFileSettings ?? new FileSaveConfigurationModel();
            this.Connection = Properties.Settings.Default.ConnectionSettings ?? new ConnectionModel();
            this.PromptOnZero = Properties.Settings.Default.PromptZeroReading;
            if (this.Debug)
            {
                Program.LoggerSwitch.MinimumLevel = Serilog.Events.LogEventLevel.Debug;
            }
            logger.Debug(
                "Read User Settings: file:'{file}',testdevice:{testdev},serialport:{portnum},timeout:{tout},debug:{debug}",
                FileSave?.FileName ?? "", AllowTestDevice, SelectedSerialPort?.PortName ?? "", Connection?.ConnectTimeout ?? 5, Debug);
            //ReadSavedFile();
        }

        internal void SaveState()
        {
            logger.Debug("Writing User Settings...");
            Properties.Settings.Default.AllowTestDevice = this.AllowTestDevice;
            Properties.Settings.Default.LastPortSelected = this.SelectedSerialPort;
            Properties.Settings.Default.Debug = this.Debug;
            Properties.Settings.Default.ScaleFileSettings = this.FileSave;
            Properties.Settings.Default.ConnectionSettings = this.Connection;
            Properties.Settings.Default.PromptZeroReading = this.PromptOnZero;
            logger.Debug(
    "Saving User Settings: file:'{file}',testdevice:{testdev},serialport:{portnum},timeout:{tout},debug:{debug}",
    FileSave?.FileName ?? "", AllowTestDevice, SelectedSerialPort?.PortName ?? "", Connection?.ConnectTimeout ?? 5, Debug);
            Properties.Settings.Default.Save();
            SaveFile();
        }

        public void UpdateModel()
        {
            FindSerialPorts();
        }


        public void FindSerialPorts()
        {
            this.SerialPorts = SerialPortUtilities.DetectSerialPorts(AllowTestDevice);
            if (SerialPorts.Count == 0)
            {
                SerialPorts.Add(Constants.NullCOM);
            }
        }

        public bool ConnectToDevice(SerialPortValue selectedItem)
        {
            if (Constants.NullCOM.Equals(selectedItem))
                throw new InvalidOperationException("No devices to connect to...");
            this.SelectedSerialPort = selectedItem;
            if (Connection.ConnectToDevice(selectedItem))
            {
                if (this.Readings == null)
                {
                    logger.Debug("Reset readings.");
                    this.Readings = new List<ScaleReadingValue>();
                }
            }
            logger.Information("Connected to Device? {connected}", Connection.IsConnected);
            return Connection.IsConnected;
        }

        internal void SaveReading(ScaleReadingValue record)
        {
            if (this.Readings.ElementAtOrDefault(record.RowID) != null)
            {
                this.Readings[record.RowID].ReadingValue = record.ReadingValue;
                this.Readings[record.RowID].ReadingTimeStamp = record.ReadingTimeStamp;
            }
            else
            {
                this.Readings.Add(record);
            }
            UpdatedRecords = true;
        }

        public bool DeviceIsConnected()
        {
            return Connection.IsConnected;
        }

        public void ReadSavedFile()
        {
            Readings = FileSave.ReadSavedFile();
            UpdatedRecords = false;
        }

        public ScaleReadingValue TakeReading(int rowId, string idTxt)
        {
            logger.Debug("Attempting to take a reading...");

            var timestamp = DateTime.Now;
            var reading = this.ReadWeight();
            var record = new ScaleReadingValue
            {
                RowID = rowId,
                ReadingTimeStamp = timestamp,
                ReadingValue = reading,
                ID = idTxt
            };
            logger.Debug("Got a stable weight reading: {reading}", reading);
            return record;
        }

        public double ReadWeight()
        {
            return this.Connection.TakeStableReading();
        }

        public void SaveFile()
        {
            if ((this.Readings?.Count ?? 0) <= 0)
            {
                logger.Debug("Nothing to save, not generating CSV...");
                return;
            }
            if (!UpdatedRecords)
            { // Dont save file if the grid never showed or added data....
                logger.Debug("nothing update, not regenerating CSV...");
                return;
            }
            logger.Debug("Attempting to save readings file...");
            this.FileSave.SaveFile(Readings);
        }

        internal string UpdateFileSave(string fileName)
        {
            logger.Debug("Updating Selected File...");
            this.FileSave.FileName = fileName;
            if (this.Readings == null)
            {
                this.Readings = new List<ScaleReadingValue>();
            }
            this.Readings.Clear();
            logger.Debug("File name updated, readings cleared");
            return this.FileSave.FileName;
        }

        internal void Disconnect()
        {
            this.Connection.Disconnect();
        }
    }
}