using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdnoWindowsApp.Core
{
    internal static class GlobalConstants
    {
        public static readonly string UserPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        public static readonly IReadOnlyList<string> bitrates = new List<string>() { "Normal (128kbps)", "Good (192kbps recommended)", "Very High (256kbps)", "Ultra (384kbps)" };
        /* auto search */
        public static readonly string AUTO = "AUTO";
        /* manual search */
        public static readonly string MSEARCH = "MSEARCH";
        /* manual entry */
        public static readonly string MENTRY = "MENTRY";
        
        public static readonly string RgxPattern = @"[^\p{L}\p{N}\p{M}_ ]";

        /*
         * Settings:
         * These change at run time but will remain consistent for the duration of the application.
         * They will have default values.
         */
        public static string OdnoPath = $"{UserPath}\\Music\\odno";
        public static string Theme = "light";
        public static string DefaultCdRom = "D:\\";
        public static bool IsFFmpegInstalled = false;

    }
}
