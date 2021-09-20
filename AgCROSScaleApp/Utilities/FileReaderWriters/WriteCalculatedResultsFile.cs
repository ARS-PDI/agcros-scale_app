using AgCROSScaleApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace AgCROSScaleApp.Utilities.FileReaderWriters
{
    class WriteCalculatedResultsFile
    {

        public static void WriteCalcFile(
            List<CalculationResultModel> calcRecords,
            decimal minTol,
            decimal maxTol,
            string outFileName,
            string preFile,
            string postFile,
            string tareFile = null
            )
        {
            using (var streamwriter = new StreamWriter(outFileName))
            {
                if (!string.IsNullOrWhiteSpace(tareFile))
                {
                    streamwriter.WriteLine($"tare input file: \"{tareFile}\" ");
                }
                streamwriter.WriteLine($"pre-process input file: \"{preFile}\" ");
                streamwriter.WriteLine($"post-process input file: \"{postFile}\" ");
                streamwriter.WriteLine($"minimum tolerance: {minTol} ");
                streamwriter.WriteLine($"maximum tolerance: {maxTol} ");

                var hdrList = new List<string> { "id", "tarewt.value","tarewt.units" };
                for (int idx = 0; idx < calcRecords[0].PostWt.Count; idx++)
                {
                    hdrList.Add($"prewt.value[{idx + 1}]");
                    hdrList.Add($"prewt.unit[{idx + 1}]");
                    hdrList.Add($"postwt.value[{idx + 1}]");
                    hdrList.Add($"postwt.units[{idx + 1}]");
                    hdrList.Add($"calc.value[{idx + 1}]");
                    hdrList.Add($"calc.flag[{idx + 1}]");
                }
                streamwriter.WriteLine(string.Join(",", hdrList));
                foreach (var calcRecord in calcRecords)
                {
                    streamwriter.Write(calcRecord.Id);
                    streamwriter.Write(",");
                    streamwriter.Write(calcRecord.TareWt?.Item1 ?? null);
                    streamwriter.Write(",");
                    streamwriter.Write(calcRecord.TareWt?.Item2 ?? null);
                    for (int idx = 0; idx < calcRecord.PreWt.Count; idx++)
                    {
                        streamwriter.Write($",{calcRecord.PreWt[idx].Item1},{calcRecord.PreWt[idx].Item2}");
                        if (idx < calcRecord.PostWt.Count)
                        {
                            streamwriter.Write($",{calcRecord.PostWt[idx].Item1},{calcRecord.PostWt[idx].Item2}");
                            streamwriter.Write($",{calcRecord.CalcValue[idx].Item1},{calcRecord.CalcValue[idx].Item2}");
                        } else
                        {
                            streamwriter.Write($",,,,");
                        }
                    }
                    streamwriter.Write(Environment.NewLine);
                }
            }
        }
    }
}
