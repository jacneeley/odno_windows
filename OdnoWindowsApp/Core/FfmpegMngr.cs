using FFMpegCore;
using FFMpegCore.Enums;
using FFMpegCore.Exceptions;
using FFMpegCore.Extensions.Downloader;
using OdnoWindowsApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdnoWindowsApp.Core
{
    public static class FfmpegMngr
    {
        public static async Task<bool> InitFFMPEG() {
            GlobalFFOptions.Configure(options => options.BinaryFolder = "./");
            var options = GlobalFFOptions.Current;
            if (!await checkFFMPEG(options)) {
                OdnoException oe = new OdnoException(new Exception("FFmpeg could not be found or installed. Closing..."));
                OdnoException.CriticalException(oe, oe.Message, "FfmpegMngr.InitFFMPEG");
            }
            return true;
        }

        private static async Task<bool> checkFFMPEG(FFOptions options) {
            try
            {
                FFMpegCore.Helpers.FFMpegHelper.VerifyFFMpegExists(options);
                return true;
            }
            catch (Exception e) {
                var res = MessageBox.Show($"{e.Message}" +
                    $"\nFFMpeg is required for ODNO. " +
                    $" FFmpeg is the leading multimedia framework, able to decode, encode, transcode, mux, demux, stream, filter and play pretty much anything that humans and machines have created." +
                    $"\nYou can read more about FFmpeg here: https://ffmpeg.org/about.html" +
                    $"\n\nODNO can download FFMpeg for you (requires internet connection)." +
                    $"\nProceed?", "ODNO INFO", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (res == DialogResult.No) {
                    Application.Exit();
                }

                var b = await FFMpegDownloader.DownloadBinaries();
                return b.Any();
            }
            
        }

        /// <summary>
        /// Converts audio and applies metadata supplied via song.
        /// Saves converted audio file to destination (dest).
        /// </summary>
        /// <param name="song"></param>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        /// <param name="bitrate"></param>
        /// <param name="codec"></param>
        /// <exception cref="FFMpegException"></exception>
        /// <exception cref="FFMpegArgumentException"></exception>
        public static async Task<bool> ConvertToMp3WithFFMPEGCore(Song song, string source, string dest, string bitrate = "Good", string codec = "copy") {
            try
            {
                AudioQuality selectedQuality;
                switch (bitrate)
                {
                    case "Ultra":
                        selectedQuality = AudioQuality.Ultra; break;
                    case "Very High":
                        selectedQuality = AudioQuality.VeryHigh; break;
                    case "Good":
                        selectedQuality = AudioQuality.Good; break;
                    case "Normal":
                        selectedQuality = AudioQuality.Normal; break;
                    default:
                        selectedQuality = AudioQuality.Normal; break;
                }

                return await FFMpegArguments
                    .FromFileInput(source)
                    .OutputToFile($"{dest}", true, options =>
                    options.WithAudioBitrate(selectedQuality)
                    .WithAudioCodec(codec)
                    .WithCustomArgument($"-metadata title=\"{song.Title}\"")
                    .WithCustomArgument($"-metadata artist=\"{song.Artist}\"")
                    .WithCustomArgument($"-metadata album=\"{song.Album}\"")
                    .WithCustomArgument($"-metadata album_artist=\"{song.AlbumArtist}\"")
                    .WithCustomArgument($"-metadata genre=\"{song.Genre}\"")
                    .WithCustomArgument($"-metadata year=\"{song.Year}\"")
                    .WithCustomArgument($"-metadata date=\"{song.Year}\"")
                    .WithCustomArgument($"-metadata track=\"{song.TrackNum}\""))
                    .ProcessAsynchronously(true);

            }
            catch (FFMpegArgumentException)
            {
                throw;
            }
            catch (FFMpegException)
            {
                throw;
            }
            catch
            {
                throw new FFMpegException(FFMpegExceptionType.Operation, "converter failed.");
            }
        }

        /// <summary>
        /// Grab audio file via source.
        /// Copy codec & apply metadata.
        /// Save to destination (dest).
        /// </summary>
        /// <param name="song"></param>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        /// <exception cref="FFMpegException"></exception>
        /// <exception cref="FFMpegArgumentException"></exception>
        public static async Task<bool> SaveMetaDataWithFFMPEGCore(Song song, string source, string dest) {
            try
            {
                return await FFMpegArguments
                .FromFileInput(source)
                .OutputToFile(dest, true, options => options
                    .WithAudioCodec("copy")
                    .WithCustomArgument($"-metadata title=\"{song.Title}\"")
                    .WithCustomArgument($"-metadata artist=\"{song.Artist}\"")
                    .WithCustomArgument($"-metadata album=\"{song.Album}\"")
                    .WithCustomArgument($"-metadata album_artist=\"{song.AlbumArtist}\"")
                    .WithCustomArgument($"-metadata genre=\"{song.Genre}\"")
                    .WithCustomArgument($"-metadata year=\"{song.Year}\"")
                    .WithCustomArgument($"-metadata date=\"{song.Year}\"")
                    .WithCustomArgument($"-metadata track=\"{song.TrackNum}\"")
                ).ProcessAsynchronously(true);
            }
            catch (FFMpegArgumentException)
            {
                throw;
            }
            catch (FFMpegException)
            {
                throw;
            }
            catch
            {
                throw new FFMpegException(FFMpegExceptionType.Operation, "converter failed.");
            }
        }
    }
}
