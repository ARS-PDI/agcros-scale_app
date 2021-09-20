using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgCROSScaleApp.Models
{
    public class GridCalcModel
    {
        public int TareWtRow { get; set; }
        public TimestampedSample TareWt { get; set; }

        public int PreWtRow { get; set; }
        public TimestampedSample PreWt { get; set; }

        public int PostWtRow { get; set; }
        public TimestampedSample PostWt { get; set; }

        public decimal? CalcValue { get; set; }
        public string CalcValueError { get; set; }

        public override string ToString()
        {
            return
                $"{TareWt?.ToString() ?? ",,"},{PreWt?.ToString() ?? ",,"},{PostWt?.ToString() ?? ",,"},{CalcValue},\"{CalcValueError}\"";
        }
    }
}
