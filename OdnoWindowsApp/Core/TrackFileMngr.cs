using FFMpegCore;
using FFMpegCore.Enums;
using FFMpegCore.Exceptions;
using OdnoWindowsApp.Model;
using System.Text.RegularExpressions;


namespace OdnoWindowsApp.Core
{
    public class TrackFileMngr
    {
        private Song? _song;
        private string? _source;
        private bool _isConvert;
        private string? _bitrate;
        private string? _type;
        private string? _dest;

        public TrackFileMngr(Song? song, string? path, bool isConvert, string? bitrate, string? type)
        {
            _song = song;
            _source = path;
            _isConvert = isConvert;
            _bitrate = bitrate.Split(" ")[0];
            _type = type;

            string parent = path.Split(path.Split("\\").Last()).First();
            string name = Regex.Replace(song.Title, "[^a-zA-Z0-9_]", "").Replace(" ", "_");
            _dest = $"{parent}completed\\{song.TrackNum}_{name}{_type}";

        }

        public async Task<bool> DoProcess()
        {
            try {
                if (_isConvert)
                {
                    return await ConvertToMP3();
                }

                return await SaveMetaData();

            } catch (OdnoException oe) {
                OdnoException.HandleException(oe, oe.Message, "TrackFileMngr.DoProcess");
                return false;
            }
        }

        private async Task<bool> SaveMetaData()
        {
            return await FfmpegMngr.SaveMetaDataWithFFMPEGCore(_song, _source, _dest);
        }

        private async Task<bool> ConvertToMP3()
        {
            try
            {
                string codec = "copy";
                if (!_type.Equals(".mp3"))
                {
                    _dest = _dest.Replace(".wav", ".mp3");
                    codec = "mp3";
                }

                var asyncRes = await FfmpegMngr.ConvertToMp3WithFFMPEGCore(_song, _source, _dest, _bitrate, codec);
                if (asyncRes) { _source = _dest; }

                return asyncRes;
            }
            catch (FFMpegArgumentException fae) {
                throw new OdnoException(fae);
            }
            catch (FFMpegException fe)
            {
                throw new OdnoException(fe);
            }
        }

        public Song Song { get;}
    }
}
