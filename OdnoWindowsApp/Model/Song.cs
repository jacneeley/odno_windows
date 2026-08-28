using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdnoWindowsApp.Model
{
    public class Song
    {
        private string _title;
        private string _artist;
        private string _album;
        private string _albumArtist;
        private int _CD;
        private string _genre;
        private int? _year;
        private int _trackNum;
        private string _cover;

		internal class SongBuilder {
			internal string title { get; set; }
            internal string artist { get; set; }
            internal string album { get; set; }
            internal string? albumArtist { get; set; } = "";
            internal int cd { get; set; } = 0;
            internal string? genre { get; set; } = "";
            internal int? year { get; set; } = 1970;
            internal int trackNum { get; set; } = 0;
            internal string? cover { get; set; } = "";

			public SongBuilder(string title, string artist, string album) {
				this.title = title;
				this.artist = artist;
				this.album = album;
			}

			//public SongBuilder Album(String val) { this.album = val; return this; }
			//public SongBuilder Artist(String val) { this.artist = val; return this; }
			public SongBuilder AlbumArtist(string val) { albumArtist = val; return this; }
			public SongBuilder CD(int val) { cd = val; return this; }
			public SongBuilder Genre(string val) { genre = val; return this; }
			public SongBuilder Year(int? val) { year = val; return this; }
			public SongBuilder TrackNum(int val) { trackNum = val; return this; }
			public SongBuilder Cover(string val) { cover = val; return this; }

			public Song build() {
				if (  title == null || title.Equals("")  ||
					 artist == null || artist.Equals("")  ||
					 album == null || album.Equals("") ) {
					throw new ArgumentException("The following are required: Title, Album, Artist...");
				}
				
				return new Song(this);
			}
        }

		private Song(SongBuilder builder) {
			_title = builder.title;
			_artist = builder.artist;
			_album = builder.album;
			_albumArtist = builder.albumArtist;
			_CD = builder.cd;
			_genre = builder.genre;
			_year = builder.year;
			_trackNum = builder.trackNum;
			_cover = builder.cover;

		}

		public string Title
		{
			get { return _title; }
			set { _title = value; }
		}

		public string Artist
		{
			get { return _artist; }
			set { _artist = value; }
		}

		public string Album
		{
			get { return _album; }
			set { _album = value; }
		}

		public string AlbumArtist
		{
			get { return _albumArtist; }
			set { _albumArtist = value; }
		}

		public int CD
		{
			get { return _CD; }
			set { _CD = value; }
		}

		public string Genre
		{
			get { return _genre; }
			set { _genre = value; }
		}

		public int? Year
		{
			get { return _year; }
			set { _year = value; }
		}

		public int TrackNum
		{
			get { return _trackNum; }
			set { _trackNum = value; }
		}

		public string Cover
		{
			get { return _cover; }
			set { _cover = value; }
		}

		override
		public string ToString() {
			return $"{_title} : [ artist={_artist}, album={_album}, genre={_genre}, year={_year}, track_num={_trackNum}, album_cover={_cover}]";
		}
	}
}
