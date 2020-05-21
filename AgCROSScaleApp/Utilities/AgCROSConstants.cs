using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgCROSScaleApp.Utilities
{
    public class AgCROSConstants
    {
        public const decimal ZeroEpsilon = 0.00001m;

        public class FileCfgConstants
        {
            public const string DefaultVarName = "SampleValue";
            // Config section delimiter
            public const string ScaleAPPConfigSectionStart = "scaleapp.config.start";
            public const string ScaleAPPConfigSectionEnd = "scaleapp.config.end";

            //Repeatability Measurement Config Section
            public const string ScaleCfgRMMRepsKey = "rmm.num-reps";
            public const string ScaleCfgRMMStdDev = "rmm.std-dev";

            //Scale Info Model Config Section
            public const string ScaleCfgSIMCommInterface = "sim.comm-interface";
            public const string ScaleCfgSIMUnits = "sim.read-units";
            public const string ScaleCfgSIMModel = "sim.model";
            public const string ScaleCfgSIMReadability = "sim.readability[mg]";
            public const string ScaleCfgSIMRepeatability = "sim.repeatability[mg]";

            // Metadata Section
            public const string MetadataSectionStart = "md.start";
            public const string MetadataSectionEnd = "md.end";

        }

    }
}
