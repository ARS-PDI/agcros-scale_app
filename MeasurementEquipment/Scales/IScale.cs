using System;
using System.Collections.Generic;
using System.Text;

namespace MeasurementEquipment.Scales
{
    public interface IScale : IDisposable
    {
        bool IsConnected { get; }
        void Connect();
        void Disconnect();
        double TakeStableReading();
        double TakeInstantReading();
    }
}
