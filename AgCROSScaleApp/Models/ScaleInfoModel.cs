using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgCROSScaleApp.Models
{
    public class ScaleInfoModel
    {
        public string ConnectionInterface { get; set; }
        public string Model { get; set; }
        public int? Unit { get; set; }
        public decimal? Readabilitymg { get; set; }
        public decimal? Repeatabilitymg { get; set; }

        public ScaleInfoModel()
        {
            ConnectionInterface = "";
            Model = "";
            Unit = null;
            Readabilitymg = null;
            Repeatabilitymg = null;
        }

        public bool ModelIsValid()
        {
            bool valid = true;
            valid = valid && !string.IsNullOrWhiteSpace(ConnectionInterface);
            valid = valid && !string.IsNullOrWhiteSpace(Model);
            valid = valid && Unit != null;
            valid = valid && !string.IsNullOrWhiteSpace(ConnectionInterface);
            valid = valid && Readabilitymg != null;
            valid = valid && Repeatabilitymg != null;
            return valid;
        }

        public static readonly List<string> InterfaceOptions =
            new List<string>() {
                "MT-SICS"
            };

    }
}
