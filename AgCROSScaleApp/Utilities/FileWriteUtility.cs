using AgCROSScaleApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgCROSScaleApp.Utilities
{
    class FileWriteUtility
    {
        public static void SaveFile(ScaleAppViewModel model)
        {
            using (var streamwriter = new StreamWriter(model.FileSave.FileName))
            {
                WriteMetadata(streamwriter, model.FileSave.MetaData);
                WriteConfigInfo(streamwriter, model);
                if (model.RepeatMeasures.NumMeasurements <= 1)
                {
                    WriteSimpleHeader(streamwriter, model.FileSave.VariableName);
                    WriteSimpleRecords(streamwriter, model.Readings);
                }
                else
                {
                    WriteRepeatMeasureHeader(streamwriter, model.FileSave.VariableName, model.Readings.Count);
                    WriteRepeatMeasureRecords(streamwriter, model.Readings);
                }
            }
        }

        private static void WriteMetadata(StreamWriter streamwriter, List<Models.KeyValuePair<string, string>> metaData)
        {
            if (metaData.Count <= 0)
            {
                return;
            }
            streamwriter.WriteLine(AgCROSConstants.FileCfgConstants.MetadataSectionStart);
            foreach (var md in metaData)
            {
                streamwriter.WriteLine($"{md.Key}:{md.Value}");
            }
            streamwriter.WriteLine(AgCROSConstants.FileCfgConstants.MetadataSectionEnd);
        }

        private static void WriteConfigInfo(StreamWriter streamwriter, ScaleAppViewModel model)
        {
            streamwriter.WriteLine(AgCROSConstants.FileCfgConstants.ScaleAPPConfigSectionStart);

            // write RMM
            WriteRepeatMeasurementSection(streamwriter, model.RepeatMeasures);
            WriteScaleInfoSection(streamwriter, model.ScaleInfo);
            streamwriter.WriteLine(AgCROSConstants.FileCfgConstants.ScaleAPPConfigSectionEnd);
        }

        private static void WriteScaleInfoSection(StreamWriter streamwriter, ScaleInfoModel scaleInfo)
        {
            streamwriter.WriteLine($"{AgCROSConstants.FileCfgConstants.ScaleCfgSIMCommInterface}: {scaleInfo.ConnectionInterface}");
            streamwriter.WriteLine($"{AgCROSConstants.FileCfgConstants.ScaleCfgSIMModel}: {scaleInfo.Model}");
            if (scaleInfo.ConnectionInterface.Contains("MT-SICS"))
            {
                streamwriter.WriteLine($"{AgCROSConstants.FileCfgConstants.ScaleCfgSIMUnits}: MT-SICS {(MeasurementEquipment.Utilities.Constants.MTSICSUnits)scaleInfo.Unit}");
            }
            streamwriter.WriteLine($"{AgCROSConstants.FileCfgConstants.ScaleCfgSIMReadability}: {scaleInfo.Readabilitymg}");
            streamwriter.WriteLine($"{AgCROSConstants.FileCfgConstants.ScaleCfgSIMRepeatability}: {scaleInfo.Repeatabilitymg}");
        }

        private static void WriteRepeatMeasurementSection(StreamWriter streamwriter, RepeatedMeasurementModel repeatMeasures)
        {
            streamwriter.WriteLine($"{AgCROSConstants.FileCfgConstants.ScaleCfgRMMRepsKey}: {repeatMeasures.NumMeasurements}");
            streamwriter.WriteLine($"{AgCROSConstants.FileCfgConstants.ScaleCfgRMMStdDev}: {repeatMeasures.StdDevLimit}");
        }

        private static void WriteSimpleHeader(StreamWriter streamwriter, string varName)
        {
            streamwriter.WriteLine($"rownumber,recordid,timestamp,\"{varName}\",unit");
        }
        private static void WriteSimpleRecords(StreamWriter streamWriter, List<ScaleReadingValue> readings)
        {
            foreach (var record in readings)
            {
                streamWriter.WriteLine(
                    $"{record.RowID}," +
                    $"{record.ID}," +
                    $"{record.Samples[0].Timestamp:o}," +
                    $"{record.Samples[0].Value}," +
                    $"{record.Samples[0].Units}");
            }
        }

        private static void WriteRepeatMeasureHeader(StreamWriter streamwriter, string varName, int count)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("rownumber,");
            sb.Append("recordid,");
            sb.Append("mean,");
            sb.Append("stddev");
            for (int idx = 0; idx < count; idx++)
            {
                sb.Append(",timestamp,");
                sb.Append($"{varName}[{idx + 1}],");
                sb.Append($"unit");
            }
            streamwriter.WriteLine(sb.ToString());
        }

        private static void WriteRepeatMeasureRecords(StreamWriter streamwriter, List<ScaleReadingValue> readings)
        {
            foreach (var record in readings)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(record.RowID).Append(",");
                sb.Append(record.ID).Append(",");
                sb.Append(record.Mean).Append(",");
                sb.Append(record.StdDev).Append(",");
                var recString = string.Join(",",
                    record.Samples.Select(
                        o => string.Join(",", o.Timestamp.ToString("o"), o.Value, $"\"{o.Units}\"")));
                sb.Append(recString);
                streamwriter.WriteLine(sb.ToString());
            }
        }




    }
}
