using AgCROSScaleApp.Models.Types;
using AgCROSScaleApp.Utilities;
using System;
using System.Collections.Generic;
using System.Management.Instrumentation;

namespace AgCROSScaleApp.Models
{

    [Serializable]
    public class FileConfigurationModel
    {
        public string FileName { get; set; }
        public string VariableName { get; set; }

        public FileTypes FileType { get; set; }
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
