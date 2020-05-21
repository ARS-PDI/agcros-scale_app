using System;
using System.Collections.Generic;
using System.Text;

namespace MeasurementEquipment.Models
{
    public class TestBalanceValidReadingResponse : IBalanceValidReadingResponse
    {
        public decimal ReadValue { get; private set; }

        public string ReadValueUnits { get; private set; }

        public string RawResponse { get; private set; }

        public TestBalanceValidReadingResponse(decimal value)
        {
            ReadValue = value;
            ReadValueUnits = "g";
            RawResponse = "Random Raw Value between 0 and 100, assume gram.";
        }

    }
}
