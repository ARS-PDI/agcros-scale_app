using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MeasurementEquipment.Models
{
    public class MTSICSBalanceValidReadingResponse : IBalanceValidReadingResponse
    {
        public decimal ReadValue { get; internal set; }
        public string ReadValueUnits { get; internal set; }
        public string RawResponse { get; internal set; }

        private static int MTSICSSampleValueTokenIndex => 2;

        private static int MTSICSSampleUnitTokenIndex => 3;

        private static int SuccessReadingTokens => 4;

        public MTSICSBalanceValidReadingResponse(string rawResponse)
        {
            RawResponse = rawResponse;
            ProcessRawResponse(rawResponse); // will throw invalid argument or error code if it discovers.
        }

        private void ProcessRawResponse(string rawResponse)
        {
            var toks = Regex.Replace(rawResponse, @"(\s)\1+", "$1").Trim().Split(' ');
            if (toks.Length != SuccessReadingTokens)
            {
                throw new Exception("Failed to get reading");
            }
            ReadValue = decimal.Parse(toks[MTSICSSampleValueTokenIndex]); // can fail if not valid double...
            ReadValueUnits = toks[MTSICSSampleUnitTokenIndex];
        }
    }
}
