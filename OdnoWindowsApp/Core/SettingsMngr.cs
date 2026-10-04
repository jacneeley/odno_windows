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

        private static string PATH;
        private readonly static string FILE = $"{GlobalConstants.projectDir}\\odno.settings.txt";

        public static readonly Dictionary<string, string> settings = new Dictionary<string, string>();

        private static void NewSettingsFile(string settingsPath, bool is_ffmpeg = false) {
            var content = new List<String>() {
                $"is_ffmpeg_installed={is_ffmpeg}",
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
        
        public static async void SettingsFile(string settingsPath = null) {
            var file = new FileInfo(settingsPath ?? FILE);
            if (!file.Exists) {
                NewSettingsFile(file.FullName, await FfmpegMngr.InitFFMPEG());
                PATH = settingsPath ?? FILE;
                return;
            }

            PATH = file.FullName;
            Read();

            if (!bool.Parse(settings["is_ffmpeg_installed"]))
            {
                bool result = await FfmpegMngr.InitFFMPEG();
                settings["is_ffmpeg_installed"] = result.ToString();
            }

            UpdateVars();
        }

        public static void Read() {
            if (!File.Exists(PATH)) {
                throw new FileNotFoundException("Settings file could not be found.");
            }
            using (StreamReader reader = File.OpenText(PATH)) {
                while (!reader.EndOfStream) {
                        var kv = reader.ReadLine().Split("=");
                        settings.Add(kv[0], kv[1]);
                }
            }
        }

        public static bool Write() {
            try
            {
                if (!File.Exists(PATH)) { throw new FileNotFoundException("Settings file could not be found. Odno cannot save changes."); }
                using (StreamWriter sw = new StreamWriter(PATH, false)) {
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

        private static bool UpdateVars() {
            bool changes = false;
            if (!GlobalConstants.OdnoPath.Equals(settings["odno_tunes_path"])) { GlobalConstants.OdnoPath = settings["odno_tunes_path"]; changes = true; }
            if (!GlobalConstants.Theme.Equals(settings["theme"])) { GlobalConstants.Theme = settings["theme"]; changes = true; }
            if (!GlobalConstants.DefaultCdRom.Equals(settings["default_cdrom"])) { GlobalConstants.DefaultCdRom = settings["default_cdrom"]; changes = true; }
            if (GlobalConstants.IsFFmpegInstalled != bool.Parse(settings["is_ffmpeg_installed"])) { GlobalConstants.IsFFmpegInstalled = bool.Parse(settings["is_ffmpeg_installed"]); changes = true; }

            if (changes) {
                Write();
            }

            return changes;
        }
    }
}
