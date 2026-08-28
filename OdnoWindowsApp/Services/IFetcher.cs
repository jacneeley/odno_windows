using OdnoWindowsApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdnoWindowsApp.Services
{
    public interface IFetcher
    {
        Task<ResponseBody> GetAlbumFromLastFM(string artist, string album);
    }
}
