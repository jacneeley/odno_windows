using FFMpegCore;
using FFMpegCore.Exceptions;
using OdnoWindowsApp.Core;
using OdnoWindowsApp.Model;
using System.Text.RegularExpressions;
using static OdnoWindowsApp.Model.LastFmObjectModel;

namespace OdnoWindowsApp.Services
{
    public class FormSrv : IFormSrv
    {
        private string? _tmp;
        private string? _albumDir;

        private readonly Fetcher _fetcher;

        public FormSrv() { 
            _fetcher = new Fetcher();
        }

        //public FormSrv(Fetcher fetcher) {
        //    _fetcher = fetcher;
        //}

        public List<Song> GetSongsToRender(LastFmObjectModel.Album response, string albumImg = "") {
            var songs = new List<Song>();
            
            try
            {
                for (int i = 0; i < response.tracks.track.Count; i++)
                {
                    var track = response.tracks.track[i];
                    string name = Regex.Replace(track.name, "[^a-zA-Z0-9_ ]", "");
                    string artist = Regex.Replace(response.artist, "[^a-zA-Z0-9_ ]", "");
                    string album = Regex.Replace(response.name, "[^a-zA-Z0-9_ ]", "");
                    Song song = new Song.SongBuilder(name, artist, album)
                        .AlbumArtist(artist)
                        .Cover(albumImg)
                        .TrackNum(i + 1)
                        .Year(response.year)
                        .build();
                    songs.Add(song);
                }

                return songs;
            }
            catch (ArgumentException ae)
            {
                OdnoException.HandleException(new OdnoException(ae.Message, ae),
                    "Likely invalid track data",
                    "RenderResponse");
                return songs;
            }
        }

        public async Task<ResponseBody> FetchAlbum(string[] albumSource) {
            var a = albumSource.Last().Split("-");
            string album = a[0].Replace("_", " ");
            string artist = a[1].Replace("_", " ");
           return await _fetcher.GetAlbumFromLastFM(artist, album);
        }

        public async Task<ResponseBody> GetAlbumFromLastFM(string artist, string album) {
            return await _fetcher.GetAlbumFromLastFM(artist, album);
        }

        public async Task<bool> Save(string albumDir, string imgUrl, string bitrate, List<TrackFileMngr> ffmpegInstances)
        {
            _albumDir = albumDir;
            _tmp = $"{albumDir}\\completed";

            if (Directory.Exists(_tmp))
            {
                foreach (var item in Directory.GetFiles(_tmp))
                {
                    try {
                        File.Delete(item);
                    } catch {
                        throw new OdnoException(new IOException($"Error deleting: {item}."));
                    }
                }
            }
            else
            {
                if (!Directory.CreateDirectory(_tmp).Exists) {
                    throw new OdnoException(new IOException($"{_tmp} could not be created."));
                }
                
            }

            Task t1 = Task.Run(() => {
                Fetcher.GetImgFromHttp(imgUrl, _tmp);
            });

            try
            {
                var options = new ParallelOptions()
                {
                    MaxDegreeOfParallelism = ffmpegInstances.Count()
                };

                await Parallel.ForEachAsync(ffmpegInstances, options, async (instance, ct) => {
                    if (!await instance.DoProcess()) {
                        ct.ThrowIfCancellationRequested();
                    }
                });
            }
            catch
            {
                throw new OdnoException("Could not process tracks.", new Exception("Parallel operation failed."));
            }
            finally {
                t1.Wait();
                t1.Dispose();
            }

            return true;
        }

        public void Rip(string albumDir)
        {
            _albumDir = albumDir;
            _tmp = $"{albumDir}\\completed";
            
            if (String.Empty.Equals(albumDir))
            {
                MessageBox.Show("odno folder could not be found.", "odno ERROR");
                return;
            }

            if (!Directory.Exists(albumDir))
            {
                try
                {
                    if (!Directory.CreateDirectory(albumDir).Exists)
                    {
                        throw new DirectoryNotFoundException(albumDir);
                    }
                }
                catch (DirectoryNotFoundException dnfe)
                {
                    MessageBox.Show($"Failed to create Directory - {dnfe.Message}");
                    return;
                }
                catch
                {
                    //caller doesn't have access
                    MessageBox.Show("An Unexpected error occurred. odno folder could not be created. Try running odno with admin privileges.", "odno ERROR");
                    return;
                }
            }

            PwrShellMngr.OpenWMPlayerRip();
        }

        public bool CleanAndMove(string albumDir) {
            if (string.IsNullOrEmpty(albumDir) || string.IsNullOrEmpty(_tmp))
            {
                _albumDir = albumDir;
                _tmp = $"{albumDir}\\completed";
            }

            try
            {
                string[] albumFiles = Directory.GetFiles(albumDir);

                if (!Directory.CreateDirectory($"{albumDir}\\CD").Exists)
                {
                    throw new OdnoException(new IOException($"Could not create {albumDir}\\CD."));
                }

                for (int i = 0; i < albumFiles.Length; i++)
                {
                    string file = albumFiles[i];
                    if (file.Split("\\").Last().ToLower().Contains("track"))
                    {
                        move(file, $"{albumDir}\\CD");
                    }
                }
            }
            catch (IOException ie) {
                OdnoException.HandleException(new OdnoException(ie), ie.Message, "FormSrv.CleanAndMove");
                return false;
            }
            catch
            {
                OdnoException oe = new OdnoException(new IOException($"Failed to clean and move files in {albumDir}"));
                OdnoException.HandleException(oe, oe.Message, "FormSrv.CleanAndMove");
                return false;
            }

            return true;
        }

        private void move(string file, string dest)
        {
            string fileName = file.Split("\\").Last();
            File.Move(file, $"{dest}\\{fileName}");
        }

        /// <summary>
        /// Reset Directory structure before 'CleanAndMove()'.
        /// This will occur if user decides to collect metadata and save on a album directory they have used in the past.
        /// </summary>
        public void Redo(string albumDir) {
            
            _albumDir = albumDir;
            _tmp = $"{albumDir}\\completed";

            string[] files = Directory.GetFiles(_tmp);
            foreach (string file in files) {
                File.Delete(file);
            }

            files = Directory.GetFiles($"{_albumDir}\\CD");
            foreach (string file in files)
            {
                move(file, _albumDir);
            }

            Directory.Delete($"{_albumDir}\\CD");
        }

        /// <summary>
        /// For testing only.
        /// </summary>
        /// <param name="album"></param>
        /// <param name="artist"></param>
        /// <returns></returns>
        public async Task<ResponseBody> Debug(string album, string artist) { 
            return await _fetcher.GetAlbumFromLastFM(artist, album);
        }
    }
}
