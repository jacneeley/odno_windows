using OdnoWindowsApp.Core;
using OdnoWindowsApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdnoWindowsApp.Services
{
    internal interface IFormSrv
    {
        void Rip(string albumDir);
        Task<bool> Save(string albumFolder, string imgUrl, string bitrate, List<TrackFileMngr> ffmpegCmds);
        Task<ResponseBody> GetAlbumFromLastFM(string artist, string album);
        Task<ResponseBody> FetchAlbum(string[] albumSource);
        List<Song> GetSongsToRender(LastFmObjectModel.Album response, string albumImg = "");

        bool CleanAndMove(string albumDir);

    }
}
