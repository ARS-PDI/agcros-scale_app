using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgCROSScaleApp
{
    [Serializable]
    public class ScaleReadingValue
    {
        public int RowID { get; set; }
        public string ID { get; set; }
        public List<TimestampedSample> Samples { get; set; }
        public decimal? Mean { get; set; }
        public decimal? StdDev { get; set; }
        public bool RepeatMeasurement { get; set; }
        public int TempID { get; set; }
    }
    [Serializable]
    public class TimestampedSample
    {
        public DateTime Timestamp { get; set; }
        public decimal Value { get; set; }
        public string Units { get; set; }
    }
}
