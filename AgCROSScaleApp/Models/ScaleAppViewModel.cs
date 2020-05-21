using AgCROSScaleApp.Utilities;
using MeasurementEquipment.Models;
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
using unvell.ReoGrid.IO.OpenXML.Schema;

namespace AgCROSScaleApp.Models
{
    public class ScaleAppViewModel
    {
        private ILogger logger;
        public FileConfigurationModel FileSave { get; set; }
        public ConnectionModel Connection { get; set; }
        public ScaleInfoModel ScaleInfo { get; set; }
        public RepeatedMeasurementModel RepeatMeasures { get; set; }
        public List<ScaleReadingValue> Readings { get; set; }

        public List<SerialPortValue> SerialPorts { get; set; }

        public SerialPortValue SelectedSerialPort { get; set; }

        public bool FileHasRead { get; set; }

        public bool UpdatedRecords { get; set; }

        public bool AllowTestDevice { get; set; }

        public bool PromptOnZero { get; set; }

        private object fileSaveLock = new object();

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
            this.AllowTestDevice = false;
            this.FileHasRead = false;
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
            if (Connection.ConnectToDevice(selectedItem, ScaleInfo))
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
            var rec = this.Readings.ElementAtOrDefault(record.RowID);
            if (rec != null)
            {
                this.Readings[record.RowID] = record;
            }
            else
            {
                if (record.RowID == -1)
                {
                    record.RowID = this.Readings.Count;
                }
                this.Readings.Add(record);
            }
            UpdatedRecords = true;
        }

        public bool DeviceIsConnected()
        {
            return Connection.IsConnected;
        }

        public ScaleReadingValue TakeReading(int rowId, string idTxt)
        {
            logger.Debug("Attempting to take a reading...");

            var timestamp = DateTime.Now;
            var reading = this.ReadWeight();
            var record = new ScaleReadingValue
            {
                RowID = rowId,
                ID = idTxt
            };
            // "new" record, so can just add the reading.
            record.Samples = new List<TimestampedSample>{
                new TimestampedSample
                {
                    Timestamp = timestamp,
                    Value = reading.ReadValue,
                    Units = reading.ReadValueUnits
                }
            };

            logger.Debug("Got a stable weight reading: {reading}", reading);
            return record;
        }

        public IBalanceValidReadingResponse ReadWeight()
        {
            return this.Connection.TakeStableReading();
        }

        public void SaveFile()
        {
            lock (fileSaveLock)
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
                FileWriteUtility.SaveFile(this);
            }
        }

        public void CreateBackup()
        {
            lock (fileSaveLock)
            {
                BackupFileUtility.CreateBackupFile(this.FileSave.FileName);
            }
        }

        internal void Disconnect()
        {
            this.Connection.Disconnect();
        }
    }
}