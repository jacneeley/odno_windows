using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace OdnoWindowsApp.Core
{
    internal static class SettingsMngr
    {

        static string _path;
        static string EXE = "C:\\Users\\jake\\source\\repos\\OdnoWindowsApp\\OdnoWindowsApp\\Properties\\resources\\odno";

        public static readonly Dictionary<string, string> settings = new Dictionary<string, string>();

        private static void NewSettingsFile(string settingsPath) {
            var content = new List<String>() {
                "is_ffmpeg_installed=false",
                "theme=light",
                "default_cdrom=D:\\",
                $"odno_tunes_path={GlobalConstants.OdnoPath}"
            };

            using (StreamWriter sw = File.CreateText($"{settingsPath}")) {
                foreach (var item in content) {
                    {
                        sw.WriteLine(item);
                        var kv = item.Split('=');
                        settings.Add(kv[0], kv[1]);
                    }
                }
            }
        }
        
        public static void SettingsFile(string settingsPath = null) {
            var file = new FileInfo(settingsPath ?? EXE + ".settings.txt");
            if (!file.Exists) {
                NewSettingsFile(file.FullName);
                _path = settingsPath ?? EXE + ".settings.txt";
                return;
            }

            _path = file.FullName;
            Read();
        }

        public static void Read() {
            if (!File.Exists(_path)) {
                throw new FileNotFoundException("Settings file could not be found.");
            }
            using (StreamReader reader = File.OpenText(_path)) {
                while (!reader.EndOfStream) {
                        var kv = reader.ReadLine().Split("=");
                        settings.Add(kv[0], kv[1]);
                }
            }

            UpdateVars();
        }

        public static bool Write() {
            try
            {
                if (File.Exists(_path)) { throw new FileNotFoundException("Settings file could not be found. Odno cannot save changes."); }
                using (StreamWriter sw = new StreamWriter(_path, false)) {
                    string line = "";
                    foreach (var item in settings)
                    {
                        line = $"{item.Key}={item.Value}";
                        sw.WriteLine(line);
                    }
                }
                return true;
            }
            catch (FileNotFoundException fnfe)
            {
                OdnoException.HandleException(new OdnoException(fnfe), "Odno cannot save changes...", "SettingsMngr.Write");
                return false;
            }
            catch {
                OdnoException.HandleException(new OdnoException(), "File could not be saved...", "SettingsMngr.Write");
                return false;
            }
        }

        private static void UpdateVars() {
            if (!GlobalConstants.OdnoPath.Equals(settings["odno_tunes_path"])) { GlobalConstants.OdnoPath = settings["odno_tunes_path"]; }
            if (!GlobalConstants.Theme.Equals(settings["theme"])) { GlobalConstants.OdnoPath = settings["theme"]; }
            if (!GlobalConstants.DefaultCdRom.Equals(settings["default_cdrom"])) { GlobalConstants.DefaultCdRom = settings["default_cdrom"]; }
        }
    }
}
