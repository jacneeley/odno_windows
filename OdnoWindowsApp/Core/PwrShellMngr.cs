using Instances;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OdnoWindowsApp.Core
{
    internal class PwrShellMngr
    {
        private static readonly string PWRSHEXE = "powershell.exe";
        private static readonly string FFMPEG = "ffmpeg.exe";
        private class ProcessStartInfoSrv { 
            private static readonly ProcessStartInfo? _instance;

            static ProcessStartInfoSrv() {
                _instance = new ProcessStartInfo() {
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
            }

            private ProcessStartInfoSrv() { }

            internal static ProcessStartInfoSrv Instance { get; } = new ProcessStartInfoSrv();

            internal ProcessStartInfo ProcessStartInfoInstance => _instance;
        }

        public static void test() {
            var processInfo = ProcessStartInfoSrv.Instance.ProcessStartInfoInstance;
            processInfo.Arguments = "Start-Sleep -Seconds 1.5; echo 'hello'";

            execute(processInfo);
        }

        public static void OpenWMPlayerRip() {
            var processInfo = ProcessStartInfoSrv.Instance.ProcessStartInfoInstance;
            processInfo.FileName = PWRSHEXE;
            processInfo.Arguments = $"Start-Process wmplayer /Task:CDAudio -Wait";

            MessageBox.Show("Complete CD rip using Windows Media Player. Click 'OK' when done.", "odno INFO", MessageBoxButtons.OK);

            execute(processInfo);
        }

        public static void FFMPEGExecute(string ffmpegCmd) {
            var processInfo = ProcessStartInfoSrv.Instance.ProcessStartInfoInstance;
            processInfo.FileName = FFMPEG;
            processInfo.Arguments = ffmpegCmd;

            execute(processInfo);
        }

        private static void execute(ProcessStartInfo info) {
            using var process = new Process();
            process.StartInfo = info;

            process.Start();

            var stdOut = process.StandardOutput.ReadToEnd();
            var stdErr = process.StandardError.ReadToEnd();

            process.WaitForExit();
        }

        /// <summary>
        /// System will terminate here - exception needs to be unchecked. 
        /// App cannot continue without theses processes working correctly.
        /// </summary>
        private static void HandleFailedProcess(Process process) {
            process.Kill();
            Exception e = new Exception($"FFMPEG failed with exit code {process.ExitCode}. Error:{process.StandardError}");
            OdnoException.CriticalException(new OdnoException(e.Message, e), process.StandardError.ToString(), process.StartInfo.Arguments);
        }
    }
}
