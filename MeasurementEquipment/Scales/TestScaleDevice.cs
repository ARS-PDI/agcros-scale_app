using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeasurementEquipment.Scales
{
    public class TestScaleDevice : IScale
    {
        private ILogger logger = Log.ForContext<TestScaleDevice>();
        public bool IsConnected { get; set; }
        private Random rand;

        public TestScaleDevice()
        {
            IsConnected = false;
            rand = new Random();
        }

        public void Connect()
        {
            logger.Debug("Test Connection Success");
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
            logger.Debug("Test Instant Reading");
            return rand.NextDouble() * 100;
        }

        public double TakeStableReading()
        {
            logger.Debug("Test Stable Reading");
            return TakeInstantReading();
        }
    }
}
