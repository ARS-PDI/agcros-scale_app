using AgCROSScaleApp.Models.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgCROSScaleApp.Models
{
    public class CalculationModel
    {
        public decimal? MinTolerance { get; set; }
        public decimal? MaxTolerance { get; set; }
        public Dictionary<string, GridCalcModel> GridResults { get; set; }

        public CalculationModel()
        {
            GridResults = new Dictionary<string, GridCalcModel>();
        }

        public bool CanSaveNewCalcReading(string Id, SampleTypes type)
        {
            if (GridResults.ContainsKey(Id))
            {
                var result = GridResults[Id];
                switch (type)
                {
                    case SampleTypes.TareSample:
                        return !(result.TareWt != null);
                    case SampleTypes.PreSample:
                        return !(result.PreWt != null);
                    case SampleTypes.PostSample:
                        return !(result.PostWt != null);
                    default:
                        return false;
                }
            }
            return true;
        }

        // true if success, false if error/bad data...
        public bool CalculateModelValue(string id, AppTypes type)
        {
            var calcModel = GridResults[id];
            if(calcModel.PreWt != null && calcModel.PostWt != null)
            {
                var preWt = calcModel.PreWt.Value;
                var postWt = calcModel.PostWt.Value;
                if (calcModel.TareWt != null)
                {
                    preWt -= calcModel.TareWt.Value;
                }
                switch (type)
                {
                    case AppTypes.DryBasisCalc:
                        calcModel.CalcValue = 
                            (preWt - postWt) / (postWt) * 100;
                        break;
                    case AppTypes.WetBasisCalc:
                        calcModel.CalcValue = (preWt - postWt) / (preWt) * 100;
                        break;
                    default:
                        return false;
                }

                calcModel.CalcValueError = "";
                if (calcModel.CalcValue < (MinTolerance ?? 0.0m))
                {
                    calcModel.CalcValueError += "Calc Value < Min;";
                }
                if (calcModel.CalcValue > (MaxTolerance ?? 100.00m))
                {
                    calcModel.CalcValueError += "Calc Value > Max;";
                }
                return true;
            }
            // if something wrong, didn't calculate correctly;
            return false;
        }

        public bool SaveCalcReading(string Id, TimestampedSample sample, SampleTypes type)
        {
            GridCalcModel calcModel = null;
            if (GridResults.ContainsKey(Id))
            {
                calcModel = GridResults[Id];
            }
            else
            {
                calcModel = new GridCalcModel()
                {
                    TareWtRow = -1,
                    PreWtRow = -1,
                    PostWtRow = -1
                };
                GridResults.Add(Id, calcModel);
            }
            AddToModel(calcModel, sample, type);
            return CheckIfCanCalculate(Id);
        }

        private bool CheckIfCanCalculate(string id)
        {
            var calcModel = GridResults[id];
            // if tarewt is null, don't care, only these two are required.
            return calcModel.PreWt != null && calcModel.PostWt != null;
        }

        private void AddToModel(GridCalcModel result, TimestampedSample sample, SampleTypes type)
        {
            switch (type)
            {
                case SampleTypes.TareSample:
                    result.TareWt = sample;
                    break;
                case SampleTypes.PreSample:
                    result.PreWt = sample;
                    break;
                case SampleTypes.PostSample:
                    result.PostWt = sample;
                    break;
                default:
                    throw new Exception("Unsupported Calc Model Sample Type");
            }
        }

        internal int FindRowID(string id, SampleTypes type)
        {
            var calcModel = GridResults[id];
            switch (type)
            {
                case SampleTypes.TareSample:
                    return calcModel.TareWtRow;
                case SampleTypes.PreSample:
                    return calcModel.PreWtRow;
                case SampleTypes.PostSample:
                    return calcModel.PostWtRow;
                default:
                    return -1;
            }
        }
        internal void SetRowId(string id, int row, SampleTypes type)
        {
            var calcModel = GridResults[id];
            switch (type)
            {
                case SampleTypes.TareSample:
                    calcModel.TareWtRow = row;
                    break;
                case SampleTypes.PreSample:
                    calcModel.PreWtRow = row;
                    break;
                case SampleTypes.PostSample:
                    calcModel.PostWtRow = row;
                    break;
            }
        }
    }
}
