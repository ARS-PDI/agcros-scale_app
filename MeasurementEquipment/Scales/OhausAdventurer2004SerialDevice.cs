using MeasurementEquipment.Models;
using System;
using System.Collections.Generic;
using Serilog;
using System.Text;
using System.Text.RegularExpressions;
using System.IO.Ports;
using MeasurementEquipment.Configurations;
using System.Threading;

namespace MeasurementEquipment.Scales
{
    public static class Ohaus2004SerialCommands
    {
        public static string ReadCurrentMode => "?";
        public static int SuccessSerialNumberTokens => 3;
        public static int SerialNumberTokenIndex => 2;
        public static string ErrorReadCurrentMode => "Error Reading Mode.";

        public static string SetPrintOnStable => "AS";
        public static int SuccessSerialNumberTokens => 3;
        public static int SerialNumberTokenIndex => 2;
        public static string ErrorSetPrintOnStable => "Error Setting Mode";
    }

    class OhausAdventurer2004SerialDevice : IScale
    {
        private ILogger logger = Log.ForContext<MTSICSSerialDevice>();

        private Regex regex = new Regex("[ ]{2,}", RegexOptions.None);
        private SerialPort serialConn;
        private readonly SerialConfiguration serialCfg;
        private TimeSpan commTimeOut;
        private readonly AutoResetEvent waitHandle = new AutoResetEvent(false);
        private string response;

        public bool IsConnected => serialConn?.IsOpen ?? false;

        public int NumberOfRetries { get; set; }

        public string ScaleSerialNumber { get; private set; }

        public string Units { get; set; }


        public OhausAdventurer2004SerialDevice(
            SerialConfiguration serialConfiguration,
            TimeSpan commTimeOut,
            int NumRetries = 2)
        {
            this.serialCfg = serialConfiguration;
            this.commTimeOut = TimeSpan.FromSeconds(Math.Max(commTimeOut.TotalSeconds, 5.0));
            NumberOfRetries = NumRetries;
        }

        public void Connect()
        {
            //conn = true;
            serialConn = new SerialPort()
            {
                PortName = serialCfg.PortName,
                BaudRate = serialCfg.BaudRate,
                DataBits = serialCfg.DataBits,
                Handshake = serialCfg.Handshake,
                Parity = serialCfg.Parity,
                StopBits = serialCfg.StopBits
            };
            serialConn.DataReceived += SerialConn_DataReceived;
            serialConn.Open();
            if (serialConn.IsOpen)
            {
                try
                {
                    // Can't get tokens here, so lets see what current units are?
                    CheckIfAlive();
                }
                catch (Exception ex)
                {
                    logger.Debug(ex.StackTrace);
                    serialConn?.Close();
                    throw;
                }
            }
        }

        private void SerialConn_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (!serialConn.IsOpen)
                return;
            try
            {
                response = regex.Replace(serialConn.ReadLine(), " ");
                logger.Debug("Received: {Response}", response);
                waitHandle.Set();
            }
            catch (Exception ex)
            {
                logger.Error($"{ex.Message}");
            }

        }

        private void CheckIfAlive()
        {
            try
            {
                SendCommand(Ohaus2004SerialCommands.ReadCurrentMode, 1, MTSICSCommands.ErrorReadingSerial);
            }
            catch (Exception)
            {
                throw;
            }
            ExtractUnits();
        }

        private void ExtractUnits()
        {
            throw new NotImplementedException();
        }

        private void SendCommand(string command, int responseLength, string errorMessage)
        {
            // at least one attempt, otherwise include number of retries.
            for (int attempt = 0; attempt < NumberOfRetries + 1; attempt++)
            {
                if (ReadWrite(command, responseLength))
                {
                    return;
                }
            }
            throw new TimeoutException(errorMessage);
        }

        private bool ReadWrite(string command, int responseLength)
        {
            serialConn.WriteLine(command);
            logger.Debug("Sending command: {Cmd}", command);
            if (waitHandle.WaitOne(commTimeOut))
            {
                // received some message, process
                // if response length < than expected, bad message,
                // otherwise let called process message.
                return !(response.Split(' ').Length < responseLength);
            }
            // didnt get a message, failed
            return false;
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }

        public IBalanceValidReadingResponse TakeStableReading()
        {
            throw new NotImplementedException();
        }

        public IBalanceValidReadingResponse TakeInstantReading()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
