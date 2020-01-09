using System;
using System.Collections.Generic;
using System.Text;

namespace MeasurementEquipment.Scales
{
    public class TestScaleDevice : IScale
    {
        public bool IsConnected { get; set; }
        private Random rand;

        public TestScaleDevice()
        {
            IsConnected = false;
            rand = new Random();
        }

        public void Connect()
        {
            IsConnected = true;
        }

        public void Disconnect()
        {
            IsConnected = false;
        }

        public void Dispose()
        {
            // nothing to dispose.
        }

        public double TakeInstantReading()
        {
            return rand.NextDouble() * 100.0;
        }

        public double TakeStableReading()
        {
            return TakeInstantReading();
        }
    }
}
