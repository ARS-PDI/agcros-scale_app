using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgCROSScaleApp
{
    static class Program
    {
        public static LoggingLevelSwitch LoggerSwitch { get; private set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            LoggerSwitch = new LoggingLevelSwitch(Serilog.Events.LogEventLevel.Warning);
            var userDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.ControlledBy(LoggerSwitch)
                .Enrich.FromLogContext()
                .WriteTo.File(
                Path.Combine(userDocs, "ScaleApp\\Logs\\app_log-.txt"), 
                retainedFileCountLimit: 31, 
                rollingInterval: RollingInterval.Day,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{SourceContext:l}] [{Level}] {Message}{NewLine}{Exception}")
                .CreateLogger();
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new ScaleAppMain());
            }
            catch (Exception ex) 
            {
                Log.Fatal(ex, "Uncaught Exception");
            }
            finally
            {
                Log.CloseAndFlush();
            }

        }
    }
}
