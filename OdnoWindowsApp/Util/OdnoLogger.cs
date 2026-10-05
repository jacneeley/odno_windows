using Microsoft.Extensions.Logging;
using OdnoWindowsApp.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OdnoWindowsApp.Util
{
    internal class OdnoLogger
    {
        internal static readonly string logDir = $"{GlobalConstants.projectDir}\\config\\.logs";
        internal static string _path = "";

        private static bool created = false;

        private static ILogger _odnoLogger = BuildOdnoLogger("Logger");

        private class OdnoLoggerSrv {
            private static readonly ILoggerFactory? _instance;

            static OdnoLoggerSrv() {
                _instance = LoggerFactory.Create(config => {
                    config.AddConsole();
                    config.SetMinimumLevel(LogLevel.Warning);
                });

                var validDate = new string(DateTime.Now.Date.ToString().Where(c => !Path.GetInvalidFileNameChars().Contains(c)).ToArray());
                _path = $"{logDir}\\OdnoLog_{validDate.Split(" ").First()}.txt";

                if (!File.Exists(_path)) {
                    var content = $"Odno Log - Created @ {DateTime.Now}";

                    using (StreamWriter sw = new StreamWriter(_path))
                    {
                        sw.WriteLine(content);
                    }
                }
            }

            private OdnoLoggerSrv() { }

            internal static OdnoLoggerSrv Instance { get; } = new OdnoLoggerSrv();

            internal ILoggerFactory OdnoLoggerInstance => _instance;

        }

        public static ILogger BuildOdnoLogger(string category) {
            return OdnoLoggerSrv.Instance.OdnoLoggerInstance.CreateLogger(category);
        }

        private static void check() {
            Directory.CreateDirectory($"{GlobalConstants.projectDir}\\config");
            Directory.CreateDirectory($"{GlobalConstants.projectDir}\\config\\.logs");
            if (Directory.CreateDirectory(logDir).Exists) { created = true; }
        }

        public static void LogInfo(string message) {
            if (!created) check();
            string finalMsg = new StringBuilder().Append($"Odno Logger - INFO - {DateTime.Now.TimeOfDay} - ").Append(message).ToString();
            using (StreamWriter sw = new StreamWriter(_path, true))
            {
                sw.WriteLine(finalMsg);
            }

            _odnoLogger.LogInformation(finalMsg);
        }

        public static void LogError(Exception oe, string message) {
            if (!created) check();
            string finalMsg = new StringBuilder().Append($"Odno Logger - ERROR @ {DateTime.Now.TimeOfDay} - ").Append(oe.StackTrace).Append(message).ToString();
            if (!File.Exists(_path)) { throw new FileNotFoundException("Settings file could not be found. Odno cannot save changes."); }
            using (StreamWriter sw = new StreamWriter(_path, true))
            {
                sw.WriteLine(finalMsg);
            }

            _odnoLogger.LogError(finalMsg); //doesn't do anything yet. Ill figure this out later.
        }
    }
}
