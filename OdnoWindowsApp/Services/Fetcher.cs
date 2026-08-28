using MetaBrainz.MusicBrainz.Interfaces.Entities;
using OdnoWindowsApp.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace OdnoWindowsApp.Services
{
    public class Fetcher
    {
        public async Task<ResponseBody> GetAlbumFromLastFM(string artist, string album) {
            string apiKey = "149be4891079005755193ba1a4b95993";
            string query = $"http://ws.audioscrobbler.com/2.0/?method=album.getinfo&api_key={apiKey}&artist={artist}&album={album}&autocorrect=1&format=json";

            ResponseBody response = new ResponseBody.ResponseBodyBuilder(query).build();

            return await response.get();
        }

        public static void GetImgFromHttp(string imgUrl, string tmpDir) {
            var httpSrv = new ResponseBody.HttpService();
            httpSrv.DownloadImage(imgUrl, tmpDir);
        }
    }
}
