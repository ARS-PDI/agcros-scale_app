using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgCROSScaleApp.Models
{
    internal class CalculationResultModel
    {
        public string Id { get; set; }
        public Tuple<decimal, string> TareWt { get; set; }
        public List<Tuple<decimal, string>> PreWt { get; set; }
        public List<Tuple<decimal, string>> PostWt { get; set; }
        public List<Tuple<decimal, string>> CalcValue { get; set; }
        public CalculationResultModel(string Id)
        {
            this.Id = Id;
            PreWt = new List<Tuple<decimal, string>>();
            PostWt = new List<Tuple<decimal, string>>();
            CalcValue = new List<Tuple<decimal, string>>();
        }
    }
}
