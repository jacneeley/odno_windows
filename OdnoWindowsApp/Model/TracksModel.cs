using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdnoWindowsApp.Model
{
    //internal class TracksModel
    //{
    //    //public record Tracks(int trackNum, string title);
    //    //public int TrackNum { get; set; }
    //    //public string Title { get; set; }

    //    //public TracksModel(int trackNum, string title)
    //    //{
    //    //    TrackNum = trackNum;
    //    //    Title = title;
    //    //}
    //}
    internal record TracksModel(int trackNum, string title);
}
