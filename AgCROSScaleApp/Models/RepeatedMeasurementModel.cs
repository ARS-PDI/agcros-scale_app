using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgCROSScaleApp.Models
{
    [Serializable]
    public class RepeatedMeasurementModel
    {
        public int? NumMeasurements { get; set; }
        public decimal? StdDevLimit { get; set; }

        public RepeatedMeasurementModel()
        {
            NumMeasurements = null;
            StdDevLimit = null;
        }

        internal bool ModelIsValid()
        {
            if (NumMeasurements.HasValue)
            {
                if (NumMeasurements > 1)
                {
                    return StdDevLimit.HasValue;
                }
                return true;
            }
            return false;

        }
    }
}
