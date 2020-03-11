using MeasurementEquipment.Configurations;
using Serilog;
using Serilog.Core;
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
        public static string ErrorReadingSerial => "Error reading the scale serial number.";

        public static string TakeInstantReading => "SI";
        public static int SuccessInstantReadingTokens => 4;
        public static int InstantReadingTokenIndex => 2;
        public static string ErrorTakingInstantReading => "Error taking an instant reading from the scale.";

        public static string TakeStableReading => "S";
        public static int SuccessStableReadingTokens => 4;
        public static int StableReadingTokenIndex => 2;
        public static string ErrorTakingStableReading => "Error taking a stable weight reading from the scale.";

        public static string PowerScaleOn => "PWR 1";
        public static int SuccessPowerOnTokens => 2;
        public static int SuccessPowerToken => 1;
        public static string ErrorPoweringOnTheScale => "Error while attempting to power on the scale.";

        public static string CancelCommand => "@";

        public static TimeSpan CancelCommandTimeOut => TimeSpan.FromSeconds(5);
        
    }

    public class MS104TSSerialCom : IScale
    {
        private ILogger logger = Log.ForContext<MS104TSSerialCom>();

        private Regex regex = new Regex("[ ]{2,}", RegexOptions.None);
        private SerialPort serialConn;
        private readonly SerialConfiguration serialCfg;
        private TimeSpan commTimeOut;
        private readonly AutoResetEvent waitHandle = new AutoResetEvent(false);
        private string response;

        public bool IsConnected => serialConn?.IsOpen ?? false;


        public int NumberOfRetries { get; set; }

        public string ScaleSerialNumber { get; private set; }

        public MS104TSSerialCom(SerialConfiguration serialConfiguration, TimeSpan commTimeOut, int NumRetries = 2)
        {
            this.serialCfg = serialConfiguration;
            this.commTimeOut = TimeSpan.FromSeconds(Math.Max(commTimeOut.TotalSeconds, 5.0));
            NumberOfRetries = NumRetries;
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

        private void Wake(TimeSpan timeout)
        {
            serialConn.WriteLine(MS104TSCommands.CancelCommand);
            logger.Debug("Attempting to wake device with cancel (@)...");
            // wait the timeout, let it do its thing
            waitHandle.WaitOne(timeout);
            // dont care about responses.
            response = "";
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
            response = regex.Replace(serialConn.ReadLine(), " ");
            logger.Debug("Received: {Response}", response);
            waitHandle.Set();
        }

        private void GetSerialNumber()
        {
            try
            {
                // attempt to wake up the device.
                Wake(MS104TSCommands.CancelCommandTimeOut);
                //SendCommand(MS104TSCommands.PowerScaleOn, MS104TSCommands.SuccessPowerOnTokens, MS104TSCommands.ErrorPoweringOnTheScale);
                //if (!CheckPowerOnSuccessful())
                //{
                //    logger.Debug("PWR command issue. Response: {response}", response);
                //    throw new Exception("Failed to power on the scale.");
                //}
                SendCommand(MS104TSCommands.ReadSerialNumber, MS104TSCommands.SuccessSerialNumberTokens, MS104TSCommands.ErrorReadingSerial);
            }
            catch (Exception)
            {
                throw;
            }
            ExtractSerialFromResponse();
        }

        private void ExtractSerialFromResponse()
        {
            var tokens = response.Split(' ');
            if (tokens.Length != MS104TSCommands.SuccessSerialNumberTokens)
            {
                throw new Exception("Failed to retrieve serial number");
            }
            ScaleSerialNumber = tokens[MS104TSCommands.SerialNumberTokenIndex];
        }

        private bool CheckPowerOnSuccessful()
        {
            var tokens = response.Split(' ');
            if (tokens.Length < MS104TSCommands.SuccessPowerOnTokens)
            {
                throw new Exception("Failed to power on the scale.");
            }
            // TODO: clean this up with constants
            string cmd = tokens[0];
            string res = tokens[1].TrimEnd('\r', '\n');
            logger.Debug("PWR command tokens: cmd-{cmd}, response-{res}", cmd, res);
            if (!cmd.Equals("PWR")) 
                return false;
            if (!res.Equals("L") && !res.Equals("A")) 
                return false;
            // got an L or A, response was good.
            return true;
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
                Wake(TimeSpan.FromMilliseconds(500));
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


        public double TakeInstantReading()
        {
            if (!IsConnected)
                throw new System.IO.IOException("No Connection Present");
            SendCommand(MS104TSCommands.TakeInstantReading,
                MS104TSCommands.SuccessInstantReadingTokens,
                MS104TSCommands.ErrorTakingInstantReading);
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
            SendCommand(MS104TSCommands.TakeStableReading,
                MS104TSCommands.SuccessStableReadingTokens,
                MS104TSCommands.ErrorTakingStableReading);
            var tokens = response.Split(' ');
            if (tokens.Length != MS104TSCommands.SuccessStableReadingTokens)
            {
                throw new Exception("Failed to get reading");
            }
            return double.Parse(tokens[MS104TSCommands.StableReadingTokenIndex]);
        }
    }
}
