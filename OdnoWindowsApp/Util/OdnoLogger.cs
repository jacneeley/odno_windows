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

        internal OdnoLogger() {
            throw new InvalidOperationException("no.");
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

            Console.WriteLine(finalMsg);
        }

        public static void LogError(Exception oe, string message) {
            if (!created) check();
            string finalMsg = new StringBuilder().Append($"Odno Logger - ERROR @ {DateTime.Now.TimeOfDay} - ").Append(oe.StackTrace).Append(message).ToString();
            
            if (!File.Exists(_path)) { throw new FileNotFoundException("Settings file could not be found. Odno cannot save changes."); }
            using (StreamWriter sw = new StreamWriter(_path, true))
            {
                sw.WriteLine(finalMsg);
            }

            Console.WriteLine(finalMsg);
        }
    }
}
