using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgCROSScaleApp
{
    public class ScaleReadingValue
    {
        public int RowID { get; set; }
        public string ID { get; set; }
        public DateTime ReadingTimeStamp { get; set; }
        public double ReadingValue { get; set; }
        public bool RepeatMeasurement { get; set; }
    }
}
