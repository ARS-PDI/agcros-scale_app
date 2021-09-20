using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MeasurementEquipment.Models
{
    class Ohaus2004ValidResponse : IBalanceValidReadingResponse
    {
        public decimal ReadValue { get; internal set; }
        public string ReadValueUnits { get; internal set; }
        public string RawResponse { get; internal set; }

        //TODO:
        // Learn how these devices respond to properly determine output
        // think its only value response, so only 1 token?
        private static int SuccessReadingTokens => 1;

        private static int SuccessfulReadingValue => 0;


        public Ohaus2004ValidResponse(string rawResponse, string units)
        {
            RawResponse = rawResponse;
            ReadValueUnits = units;
            ProcessRawResponse(rawResponse); // will throw invalid argument or error code if it discovers.
        }

        private void ProcessRawResponse(string rawResponse)
        {
            var toks = Regex.Replace(rawResponse, @"(\s)\1+", "$1").Trim().Split(' ');
            if (toks.Length != SuccessReadingTokens)
            {
                throw new Exception("Failed to get reading");
            }
            ReadValue = decimal.Parse(toks[SuccessfulReadingValue]); // can fail if not valid double...
        }
    }
}
