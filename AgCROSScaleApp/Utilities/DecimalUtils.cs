using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgCROSScaleApp.Utilities
{
    class DecimalUtils
    {

        public static int GetNumPlacesAfterPoint(decimal value)
        {
            var strVal = value.ToString();
            if (!strVal.Contains("."))
            {
                return 0;
            }
            else
            {
                return value.ToString().Split('.')[1].Length;
            }
        }

        public static int GetNumPlacesAfterPoint(decimal? value)
        {
            if (!value.HasValue)
                return 0;
            else
                return GetNumPlacesAfterPoint(value.Value);
        }
    }
}
