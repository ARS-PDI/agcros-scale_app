using MeasurementEquipment.Configurations;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace MeasurementEquipment.Scales
{
    public static class MS104TSCommands
    {
        public static string ReadSerialNumber => "I4";
        public static int SuccessSerialNumberTokens => 3;
        public static int SerialNumberTokenIndex => 2;
        public static string TakeInstantReading => "SI";
        public static int SuccessInstantReadingTokens => 4;
        public static int InstantReadingTokenIndex => 2;

        public static string TakeStableReading => "S";
        public static int SuccessStableReadingTokens => 4;
        public static int StableReadingTokenIndex => 2;

        public static string PowerScaleOn => "PWR 1";
        public static int SuccessPowerOnTokens => 2;
        public static int SuccessPowerToken => 1;

    }


    public class MS104TSSerialCom : IScale
    {
        private Regex regex = new Regex("[ ]{2,}", RegexOptions.None);
        private SerialPort serialConn;
        private readonly SerialConfiguration serialCfg;
        private TimeSpan commTimeOut;
        private readonly AutoResetEvent waitHandle = new AutoResetEvent(false);
        private string response;

        public int NumberOfRetries { get; set; }

        public string ScaleSerialNumber { get; private set; }

        public MS104TSSerialCom(SerialConfiguration serialConfiguration, TimeSpan commTimeOut)
        {
            this.serialCfg = serialConfiguration;
            this.commTimeOut = commTimeOut;
            NumberOfRetries = 2;
        }

        public void Dispose()
        {
            if (serialConn != null)
            {
                if (serialConn.IsOpen)
                {
                    serialConn.Close();
                }
            }
        }

        public void Disconnect()
        {
            if (serialConn != null)
            {
                if (serialConn.IsOpen)
                {
                    serialConn.Close();
                }
            }
        }

        /// <exception cref="UnauthorizedAccessException">Serial Port Open Exception</exception>
        /// <exception cref="ArgumentOutOfRangeException">Serial Port Open Exception</exception>
        /// <exception cref="ArgumentException">Serial Port Open Exception</exception>
        /// <exception cref="System.IO.IOException">Serial Port Open Exception</exception>
        /// <exception cref="InvalidOperationException">Serial Port Open Exception</exception>
        public void Connect()
        {
            //conn = true;
            //TODO: Test the following wiht a real scale
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
                    GetSerialNumber();
                }
                catch (Exception)
                {
                    serialConn?.Close();
                    throw;
                }
            }
        }

        private void SerialConn_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (!serialConn.IsOpen)
                return;
            response = regex.Replace(serialConn.ReadLine(), " ");
            waitHandle.Set();
        }

        private void GetSerialNumber()
        {
            try
            {
                SendCommand(MS104TSCommands.PowerScaleOn, MS104TSCommands.SuccessPowerOnTokens);
                SendCommand(MS104TSCommands.ReadSerialNumber, MS104TSCommands.SuccessSerialNumberTokens);
            }
            catch (Exception)
            {
                throw;
            }
            var tokens = response.Split(' ');
            if (tokens.Length != MS104TSCommands.SuccessSerialNumberTokens)
            {
                throw new Exception("Failed to retrieve serial number");
            }
            ScaleSerialNumber = tokens[MS104TSCommands.SerialNumberTokenIndex];
        }

        private void SendCommand(string command, int responseLength)
        {
            for (var i = 0; i < NumberOfRetries; i++)
            {
                serialConn.WriteLine(command);
                if (waitHandle.WaitOne(commTimeOut))
                {
                    // received some message, process
                    if (response.Split(' ').Length < responseLength
                        && i != NumberOfRetries - 1)
                    {
                        continue;
                    }
                    return;
                }
            }
            throw new TimeoutException("Timed out attempting to get scale serial number");
        }

        public bool IsConnected => serialConn?.IsOpen ?? false;

        public double TakeInstantReading()
        {
            if (!IsConnected)
                throw new System.IO.IOException("No Connection Present");
            SendCommand(MS104TSCommands.TakeInstantReading, MS104TSCommands.SuccessInstantReadingTokens);
            var tokens = response.Split(' ');
            if (tokens.Length != MS104TSCommands.SuccessInstantReadingTokens)
            {
                throw new Exception("Failed to get reading");
            }
            return double.Parse(tokens[MS104TSCommands.InstantReadingTokenIndex]);
        }

        public double TakeStableReading()
        {
            if (!IsConnected)
                throw new System.IO.IOException("No Connection Present");
            SendCommand(MS104TSCommands.TakeStableReading, MS104TSCommands.SuccessStableReadingTokens);
            var tokens = response.Split(' ');
            if (tokens.Length != MS104TSCommands.SuccessStableReadingTokens)
            {
                throw new Exception("Failed to get reading");
            }
            return double.Parse(tokens[MS104TSCommands.StableReadingTokenIndex]);
        }
    }
}
