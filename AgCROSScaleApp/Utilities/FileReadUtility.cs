using AgCROSScaleApp.Models.Types;
using AgCROSScaleApp.Utilities;
using MeasurementEquipment.Utilities;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using unvell.ReoGrid.Drawing.Shapes;

namespace AgCROSScaleApp.Models
{
    class FileReadUtility
    {
        private static ILogger logger = Log.ForContext<FileReadUtility>();

        // Read in file
        public static void ReadSavedFile(ScaleAppViewModel model, string fileName)
        {
            model.Readings = new List<ScaleReadingValue>();
            model.RepeatMeasures = new RepeatedMeasurementModel();
            model.ScaleInfo = new ScaleInfoModel();
            model.FileSave = new FileConfigurationModel()
            {
                FileName = fileName
            };
            model.CalcModel = new CalculationModel();
            // if file exists, read all the info in
            // Otherwise -> start fresh.
            if (File.Exists(fileName))
            {
                logger.Debug("Attempting to read in file, adding records as they are found...");
                using (var streamReader = new StreamReader(model.FileSave.FileName))
                {
                    var line = ReadMetadata(streamReader, model.FileSave);
                    line = ReadAppConfigSection(streamReader, model, line);
                    var numMeasurements = model.RepeatMeasures.NumMeasurements;
                    model.FileSave.VariableName = ReadCustomVariableName(line, model.FileSave.FileType);
                    // last line should be header
                    while (!streamReader.EndOfStream)
                    {
                        var tokenLine = streamReader.ReadLine().Split(',');
                        if (tokenLine.Length > 3)
                        {
                            if (model.FileSave.FileType == FileTypes.CalculatedValue)
                            {
                                ReadCalculationFileType(model.CalcModel, tokenLine);
                            }
                            else
                            {
                                var reading = new ScaleReadingValue
                                {
                                    RowID = int.Parse(tokenLine[0]),
                                    ID = tokenLine[1],
                                    RepeatMeasurement = false,
                                    Samples = new List<TimestampedSample>()
                                };

                                switch (model.FileSave.FileType)
                                {
                                    case FileTypes.SingleReading:
                                        reading.Samples.Add(ReadSingleMeasurementData(tokenLine));
                                        break;
                                    case FileTypes.MultiReading:
                                        ReadRepeatedSampleData(tokenLine, reading, numMeasurements.Value);
                                        break;
                                    default:
                                        break;
                                }
                                model.Readings.Add(reading);
                            }

                        }
                    }
                }
            }
        }

        private static void ReadCalculationFileType(CalculationModel cm, string[] tokenLine)
        {
            var id = tokenLine[0];
            var newGridRes = new GridCalcModel()
            {
                PostWtRow = -1,
                PreWtRow = -1,
                TareWtRow = -1
            };

            if (tokenLine.Length > 1)
            {
                newGridRes.TareWt = CreateTimestampedSample(tokenLine, 1, 2, 3);
                newGridRes.PreWt = CreateTimestampedSample(tokenLine, 4, 5, 6);
                newGridRes.PostWt = CreateTimestampedSample(tokenLine, 7, 8, 9);
                newGridRes.CalcValue = TryParseDecimalToken(tokenLine, 10);
                newGridRes.CalcValueError = tokenLine[11].Trim();
            }

            // will overwrite dups, which won't work for this app...
            cm.GridResults[id] = newGridRes;
        }

        private static TimestampedSample ReadSingleMeasurementData(string[] tokenLine)
        {
            return CreateTimestampedSample(tokenLine, 2, 3, 4);
        }

        private static TimestampedSample CreateTimestampedSample(string[] tokenLine, int dateIndex, int valueIndex, int unitIndex)
        {
            if (string.IsNullOrWhiteSpace(tokenLine[dateIndex])
                && string.IsNullOrWhiteSpace(tokenLine[valueIndex])
                && string.IsNullOrWhiteSpace(tokenLine[unitIndex].Trim('"')))
            {
                return null;
            }
            DateTime date = (!string.IsNullOrWhiteSpace(tokenLine[dateIndex])) ? DateTime.Parse(tokenLine[dateIndex]) : default;
            var value = TryParseDecimalToken(tokenLine, valueIndex) ?? default;
            return new TimestampedSample()
            {
                Timestamp = date,
                Value = value,
                Units = tokenLine[unitIndex].Trim('"')
            };
        }

        private static void ReadRepeatedSampleData(string[] tokenLine, ScaleReadingValue reading, int numReps)
        {
            decimal dparse;
            reading.Mean = decimal.TryParse(tokenLine[2], out dparse) ? (decimal?)dparse : null;
            reading.StdDev = decimal.TryParse(tokenLine[3], out dparse) ? (decimal?)dparse : null;
            for (int idx = 0; idx < numReps; idx++)
            {
                if (tokenLine.Length > (4 + idx * 3))
                {
                    reading.Samples.Add(CreateTimestampedSample(tokenLine, 4 + (idx * 3), 5 + (idx * 3), 6 + (idx * 3)));
                }
            }
        }

        private static string ReadMetadata(StreamReader streamReader, FileConfigurationModel model)
        {
            var line = streamReader.ReadLine();
            if (line.Contains(AgCROSConstants.FileCfgConstants.MetadataSectionStart))
            {
                string[] tokens;
                while (!streamReader.EndOfStream && !line.ToLower().Contains(AgCROSConstants.FileCfgConstants.MetadataSectionEnd))
                {
                    line = streamReader.ReadLine();
                    tokens = line.Split(':');
                    if (tokens.Length > 1)
                    {
                        model.MetaData.Add(new KeyValuePair<string, string>() { Key = tokens[0], Value = tokens[1] });
                    }
                }
                // read next line to get rid of MD section end.
                line = streamReader.ReadLine();
            }
            return line;
        }

        private static string ReadAppConfigSection(StreamReader streamReader, ScaleAppViewModel model, string line)
        {
            // if line contains config start, then it is app config, otherwise it is the header
            if (line.Contains(AgCROSConstants.FileCfgConstants.ScaleAPPConfigSectionStart))
            {
                while (!streamReader.EndOfStream && !line.ToLower().Contains(AgCROSConstants.FileCfgConstants.ScaleAPPConfigSectionEnd))
                {
                    line = streamReader.ReadLine();
                    var tokens = line.Split(':');
                    if (line.Contains(AgCROSConstants.FileCfgConstants.ScaleCfgFileType))
                    {
                        model.FileSave.FileType = (FileTypes)Enum.Parse(typeof(FileTypes), tokens[1]);
                    }
                    else if (line.Contains(AgCROSConstants.FileCfgConstants.ScaleCfgAppType))
                    {
                        model.AppType = (AppTypes)Enum.Parse(typeof(AppTypes), tokens[1]);
                    }
                    else if (line.Contains(AgCROSConstants.FileCfgConstants.ScaleCfgCMTolMin))
                    {
                        model.CalcModel.MinTolerance = TryParseDecimalToken(tokens, 1);
                    }
                    else if (line.Contains(AgCROSConstants.FileCfgConstants.ScaleCfgCMTolMax))
                    {
                        model.CalcModel.MaxTolerance = TryParseDecimalToken(tokens, 1);
                    }
                    else if (line.Contains(AgCROSConstants.FileCfgConstants.ScaleCfgRMMRepsKey))
                    {
                        model.RepeatMeasures.NumMeasurements = int.Parse(tokens[1]);
                    }
                    else if (line.Contains(AgCROSConstants.FileCfgConstants.ScaleCfgRMMStdDev))
                    {
                        model.RepeatMeasures.StdDevLimit = TryParseDecimalToken(tokens, 1);
                    }
                    else if (line.Contains(AgCROSConstants.FileCfgConstants.ScaleCfgSIMCommInterface))
                    {
                        model.ScaleInfo.ConnectionInterface = tokens[1].Trim();
                    }
                    else if (line.Contains(AgCROSConstants.FileCfgConstants.ScaleCfgSIMUnits))
                    {
                        var subTokens = tokens[1].Trim().Split(' ');
                        if (subTokens[0].Trim().Contains("MT-SICS"))
                        {
                            model.ScaleInfo.Unit = (int)Enum.Parse(typeof(Constants.ScaleUnits), subTokens[1].Trim(), true);
                        }
                    }
                    else if (line.Contains(AgCROSConstants.FileCfgConstants.ScaleCfgSIMModel))
                    {
                        model.ScaleInfo.Model = tokens[1].Trim();
                    }
                    else if (line.Contains(AgCROSConstants.FileCfgConstants.ScaleCfgSIMReadability))
                    {
                        model.ScaleInfo.Readabilitymg = TryParseDecimalToken(tokens, 1);
                    }
                    else if (line.Contains(AgCROSConstants.FileCfgConstants.ScaleCfgSIMRepeatability))
                    {
                        model.ScaleInfo.Repeatabilitymg = TryParseDecimalToken(tokens, 1);
                    }
                }
                // read next line, should be data header
                line = streamReader.ReadLine();
            }
            return line;
        }

        private static decimal? TryParseDecimalToken(string[] tokens, int index)
        {
            // only try for indices 0 <= index <= length-1
            if (tokens.Length < index + 1 || index < 0)
                return null;
            if (decimal.TryParse(tokens[index], out decimal value))
            {
                return value;
            }
            return null;
        }

        public static string ReadCustomVariableName(string line, FileTypes type)
        {
            var tokens = line.Split(',');

            switch (type)
            {
                case FileTypes.SingleReading:
                    if (tokens.Length > 3)
                    {
                        return tokens[3].Trim('"');
                    }
                    break;
                case FileTypes.MultiReading:
                    if (tokens.Length >= 5)
                    {
                        return tokens[5].Trim('"').Split('[')[0];
                    }
                    break;
                case FileTypes.CalculatedValue:
                    if (tokens.Length >= 5)
                    {
                        return tokens[2].Trim('"').Split('_')[1].Trim('"');
                    }
                    break;
            }

            return AgCROSConstants.FileCfgConstants.DefaultVarName;
        }
    }
}
