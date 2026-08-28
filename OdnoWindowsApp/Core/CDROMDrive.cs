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

                                drivePath = drive.Name.Replace("\\", "");
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
            if (string.Empty.Equals(drivePath)) {
                return false;
            }

            Ejector.Eject(drivePath);

            Thread.Sleep(500);
            
            bool reloaded = watchForDrive(drivePath);

            return reloaded;
        }

        public static bool watchForDrive(string drive) {
            int start = DateTime.Now.Second;
            int end = 0;
            bool run = true;
            while (run) {
                try
                {
                    run = !DriveInfo.GetDrives().Where(d => d.DriveType.Equals(DriveType.CDRom)).Where(d => d.IsReady).Any();

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
                catch (TimeoutException te) {
                    OdnoException.HandleException(new OdnoException(te), te.Message, "CDROMDrive.watchForDrive");
                    return false;
                }
            }
            return true;
        }

        private class Ejector {
            // Constants used in DLL methods
            const int OPEN_EXISTING = 3;
            const uint GENERIC_READ = 0x80000000;
            const uint GENERIC_WRITE = 0x40000000;
            const uint IOCTL_STORAGE_EJECT_MEDIA = 2967560;
            const uint FILE_SHARE_READ = 0x00000001; 
            const uint FILE_SHARE_WRITE = 0x00000002;

            [DllImport("kernel32")]
            private static extern nint CreateFile(
                string filename, uint desiredAccess,
                uint shareMode, nint securityAttributes,
                int creationDisposition, int flagsAndAttributes,
                nint templateFile
            );

            [DllImport("kernel32")]
            private static extern int DeviceIoControl
                (nint deviceHandle, uint ioControlCode,
                 nint inBuffer, int inBufferSize,
                 nint outBuffer, int outBufferSize,
                 ref int bytesReturned, nint overlapped);

            [DllImport("kernel32")]
            private static extern int CloseHandle(nint handle);

            internal static void Eject(string drive) {
                bool isException = false;
                string f = $"\\\\.\\{drive}";
                nint handle = CreateFile(f, GENERIC_READ | GENERIC_WRITE,
                    FILE_SHARE_READ | FILE_SHARE_WRITE, nint.Zero, OPEN_EXISTING, 0, nint.Zero);

                try
                {
                    if ((long)handle == -1)
                    {
                        isException = true;
                        throw new IOException($"Unable to open {drive}...");
                    }
                    int holder = 0;
                    int success = DeviceIoControl(handle, IOCTL_STORAGE_EJECT_MEDIA, nint.Zero, 0,
                        nint.Zero, 0, ref holder, nint.Zero);

                    if (success == 0)
                    {
                        
                        throw new IOException("Eject failed");
                    }
                }
                catch (IOException io)
                {
                    //handle
                    isException = true;
                    OdnoException.HandleException(new OdnoException(io.Message, io), "Problem w/CDROMDrive", "CDROMDrive.Ejector.Eject");
                }

                finally
                {
                    CloseHandle(handle);
                    if (isException) {
                        Application.Exit();
                    }
                }
            }
        }
    }
}
