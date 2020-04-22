using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AgCROSScaleApp.Models
{
    [Serializable]
    public class FileSaveConfigurationModel
    {
        private ILogger logger = Log.ForContext<FileSaveConfigurationModel>();

        public string FileName { get; set; }
        public string VariableName { get; set; }
        public List<KeyValuePair<string, string>> MetaData { get; set; }

        public FileSaveConfigurationModel()
        {
            FileName = "";
            VariableName = "Reading";
            MetaData = new List<KeyValuePair<string, string>>();
        }

        public void SaveFile(List<ScaleReadingValue> readings)
        {
            if (string.IsNullOrWhiteSpace(this.FileName))
                return;
            using (var streamwriter = new StreamWriter(this.FileName))
            {
                WriteMetadata(streamwriter);
                WriteReadingRecords(readings, streamwriter);
            }

        }

        private void WriteReadingRecords(List<ScaleReadingValue> readings, StreamWriter streamwriter)
        {
            streamwriter.WriteLine($"rownumber,recordid,timestamp,\"{this.VariableName}\"");
            foreach (var record in readings)
            {
                streamwriter.WriteLine($"{record.RowID},{record.ID},{record.ReadingTimeStamp.ToString("o")},{record.ReadingValue}");
            }
        }

        private void WriteMetadata(StreamWriter streamwriter)
        {
            if (this.MetaData.Count <= 0)
            {
                return;
            }
            streamwriter.WriteLine("md.start");
            foreach (var md in MetaData)
            {
                streamwriter.WriteLine($"{md.Key}:{md.Value}");
            }
            streamwriter.WriteLine("md.end");
        }

        public List<ScaleReadingValue> ReadSavedFile()
        {
            var readings = new List<ScaleReadingValue>();
            if (string.IsNullOrWhiteSpace(this.FileName))
            {
                logger.Debug("Attempting to read in file: no file selected.");
                return readings;
            }

            if (!File.Exists(this.FileName))
            {
                logger.Debug("Attempting to read in file: file does not exist.");
                return readings;
            }
            logger.Debug("Attempting to read in file, adding records as they are found...");
            using (var streamReader = new StreamReader(this.FileName))
            {
                DiscardHeader(streamReader);
                while (!streamReader.EndOfStream)
                {
                    var tokenLine = streamReader.ReadLine().Split(',');
                    if (tokenLine.Length > 3)
                    {
                        readings.Add(new ScaleReadingValue
                        {
                            RowID = int.Parse(tokenLine[0]),
                            ID = tokenLine[1],
                            ReadingTimeStamp = DateTime.Parse(tokenLine[2]),
                            ReadingValue = double.Parse(tokenLine[3]),
                            RepeatMeasurement = false
                        });
                    }
                }
            }
            return readings;
        }


        public Tuple<List<KeyValuePair<string, string>>, string> OpenFileAndExtractMetadata(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                logger.Debug("Attempting to read in file: no file selected.");
            }
            if (!File.Exists(fileName))
            {
                logger.Debug("Attempting to read in file: file does not exist.");
            }
            logger.Debug("Attempting to read in file, adding records as they are found...");
            var newMD = new List<KeyValuePair<string, string>>();
            var newVarName = "Reading";
            using (var streamReader = new StreamReader(fileName))
            {
                string line = streamReader.ReadLine();


                if (line.Contains("md.start"))
                {
                    string[] tokens;
                    while (!streamReader.EndOfStream && !line.ToLower().Contains("md.end"))
                    {
                        line = streamReader.ReadLine();
                        tokens = line.Split(':');
                        if (tokens.Length > 1)
                        {
                            newMD.Add(new KeyValuePair<string, string>() { Key = tokens[0], Value = tokens[1] });
                        }
                        
                    }
                    line = streamReader.ReadLine();
                    tokens = line.Split(',');
                    if (tokens.Length > 3)
                    {
                        newVarName = tokens[3].Trim('"');
                    }

                }
            }
            return new Tuple<List<KeyValuePair<string, string>>, string>(newMD, newVarName);
        }

        private void DiscardHeader(StreamReader streamReader)
        {
            string line = streamReader.ReadLine(); 
            if (line.Contains("md.start"))
            {
                while (!streamReader.EndOfStream && !line.ToLower().Contains("md.end"))
                {
                    line = streamReader.ReadLine();
                }
                line = streamReader.ReadLine(); // dump the header after MD end.
            }
            // header was already dumped if no MD in file.
        }
    }
}
