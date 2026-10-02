using OdnoWindowsApp.CDLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static OdnoWindowsApp.Model.LastFmObjectModel;

namespace OdnoWindowsApp.Core
{
    internal class CDROMDrive
    {
        private static string drivePath = "";

        public static bool isReady = false;
        public static FileInfo[]? tracks;
        public static string dvdRoot = "";
        public static DriveInfo? _drive;
        public static List<string> CDROMS = new List<string>();

        private static readonly CDDrive _cdDrive = new();
        
        public static bool LoadDisc(string? reloadStr = null)
        {
            try
            {
                var drives = DriveInfo.GetDrives().Where(d => d.DriveType.Equals(DriveType.CDRom));

                if (drives.Any())
                {
                    foreach (var drive in drives)
                    {
                        if (drive.IsReady) {
                            if (!CDROMS.Any() || CDROMS.Contains(drive.Name)) { CDROMS.Add(drive.Name); }

                            if (drive.Name.Equals(GlobalConstants.DefaultCdRom))
                            {
                                tracks = drive.RootDirectory.GetFiles();

                                drivePath = drive.Name.Split(":").First();
                                dvdRoot = $"({drivePath}) {drive.VolumeLabel}";
                                isReady = drive.IsReady;
                                _drive = drive;

                                //if (!string.IsNullOrEmpty(reloadStr))
                                //{
                                //    return isReady;
                                //}
                                return isReady;
                            }
                        }
                        else
                        {
                            MessageBox.Show("CD/DVD Drive is empty. Load a CD into the drive.", "odno INFO");
                        }

                        return isReady;

                    }
                }
                MessageBox.Show("No CD/DVD Drive was found.", "odno INFO");
                return false;
            }
            catch (IOException ioe)
            {
                OdnoException.HandleException(new OdnoException(ioe.Message, ioe), "IO issue...", "CDROMDISC.LoadDisc");
                return false;
            }
        }

        public static bool OpenClose() {
            if (_cdDrive.Toggler) { _cdDrive.Close(); }
            else {
                if (string.Empty.Equals(drivePath))
                {
                    return false;
                }
                
                _cdDrive.Open(char.Parse(drivePath)); 
            }

            isReady = watchForDrive(drivePath);
            return isReady;
        }

        public static bool watchForDrive(string drive) {
            Thread.Sleep(1000); //give it a sec
            int start = DateTime.Now.Second;
            int end = 0;
            while (true)
            {
                try
                {
                    if (_cdDrive.IsCDReady())
                    {
                        return true;
                    }

                    Thread.Sleep(250);

                    end = DateTime.Now.Second;
                    if (end - start >= 90)
                    {
                        throw new TimeoutException("Timeout: Drive could not be found. User most likely left drive open for too long or some other issue.");
                    }
                }
                catch (IOException ioe)
                {
                    OdnoException.CriticalException(new OdnoException(ioe), ioe.Message, "CDROMDrive.watchForDrive");
                }
                catch (TimeoutException te)
                {
                    OdnoException.HandleException(new OdnoException(te), te.Message, "CDROMDrive.watchForDrive");
                    return false;
                }
            }
        }

        public static async Task Rip(string dest = "") {
            try
            {
                _cdDrive.ReadDisc(char.Parse(drivePath));

                if ("".Equals(dest))
                {
                    dest = GlobalConstants.OdnoPath;
                }

                if (!await _cdDrive.RipContents(dest))
                {
                    throw new IOException("Track data was not present or could not be accessed...");
                }
            }
            catch (Exception e) {
                throw;
            }
        }
    }
}
