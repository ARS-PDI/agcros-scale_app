using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgCROSScaleApp.Models.Types
{
    public enum AppTypes
    {
        [Description("No Calculation")]
        NoCalculation,
        [Description("Estimate SWC: Wet Weight")]
        WetBasisCalc,
        [Description("Estimate SWC: Dry Weight")]
        DryBasisCalc
    }
}
