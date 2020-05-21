using System;
using System.Collections.Generic;
using System.Text;

namespace MeasurementEquipment.Models
{
    public interface IBalanceValidReadingResponse
    {
        decimal ReadValue { get; }
        string ReadValueUnits { get; }
        string RawResponse { get; }
    }
}
