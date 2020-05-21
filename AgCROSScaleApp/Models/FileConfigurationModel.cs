using AgCROSScaleApp.Utilities;
using MeasurementEquipment.Utilities;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Windows.Forms;

namespace AgCROSScaleApp.Models
{
    [Serializable]
    public class FileConfigurationModel
    {
        public string FileName { get; set; }
        public string VariableName { get; set; }
        [IgnoreMember]
        public List<KeyValuePair<string, string>> MetaData { get; set; }

        public FileConfigurationModel()
        {
            FileName = "";
            VariableName = AgCROSConstants.FileCfgConstants.DefaultVarName;
            MetaData = new List<KeyValuePair<string, string>>();
        }
    }
}
