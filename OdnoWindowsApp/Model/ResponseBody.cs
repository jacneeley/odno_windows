using MetaBrainz.MusicBrainz;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OdnoWindowsApp.Core;
using OdnoWindowsApp.Util;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace OdnoWindowsApp.Model
{
    public class ResponseBody : IResponseBody
    {
		private string _url;
		private int _responseCode;
		private string _response;
		private LastFmObjectModel.Album _responseJson;
		private List<Song> _resultList;
		private List<string> _errList;
		private bool _isSuccess;

        internal class HttpService {
			private readonly HttpClient _client;
			private HttpResponseMessage? _resp = null;

			public HttpService()
			{
				_client = new HttpClient();
			}

			internal async Task<HttpResponseMessage> GetAsync(string url) {
				HttpResponseMessage resp = await _client.GetAsync(url);
				resp.EnsureSuccessStatusCode();
				_resp = resp;
				return resp;
			}

			internal async Task<LastFmObjectModel.Album> GetJsonResponse() {
				try
				{
                    if (_resp == null) return jsonFail();

                    var payload = await _resp.Content.ReadAsStringAsync();
                    LastFmObjectModel.Root? json = JsonConvert.DeserializeObject<LastFmObjectModel.Root>(payload);
					
					if (json == null) return jsonFail();

                    return json.album;
                }
				catch (Exception ex)
				{
					OdnoException.HandleException(new OdnoException(ex), ex.Message, "ResponseBody.GetResponseJson");
                    return jsonFail();
                }
			}

			private LastFmObjectModel.Album jsonFail() {
                string errJson = "{ error : json content could not be retrieved from response or response was null }";
                LastFmObjectModel.Album? failedJson = JsonConvert.DeserializeObject<LastFmObjectModel.Album>(errJson);
                return failedJson;
            }

			/// <summary>
			/// Get Release Year from musicbrainz API via mbid because for some reason LastFM doesn't give user the year anymore?
			/// </summary>
			/// <param name="mbid"></param>
			/// <returns>int year or 0 if year cannot be retrieved.</returns>
            internal async Task<int?> GetDateFromMusicBrainz(string mbid)
            {
				if (String.IsNullOrEmpty(mbid)) {
					return 0;
				}
                /*TODO: investigate 
                 * https://musicbrainz.org/doc/musicbrainz-sharp
                 * https://github.com/Zastai/MetaBrainz.MusicBrainz/blob/main/user-guide/UserGuide.md
                 * If the following works, maybe re-verse engineer to remove dependency.
                 */
                var q = new Query("odno", new Version("1.0.0"), "mailto:jankudev@tutamail.com");

				try
				{
					//TODO: maybe start a timer that causes timeout if 1 minute is exceeded...

					var release = await q.LookupReleaseAsync(new Guid(mbid), Include.ReleaseGroups);
					if (release != null && release.ReleaseGroup != null && release.ReleaseGroup.FirstReleaseDate != null)
					{
						int? date = release.ReleaseGroup.FirstReleaseDate != null
							? release.ReleaseGroup.FirstReleaseDate.Year : 1;

						q.Close();
						return date;
					}
				}
				catch (MetaBrainz.Common.HttpError mhe)
				{
					OdnoException.HandleException(new OdnoException(mhe), mhe.Message, "ResponseBody.GetDateFromMusicBrainz");
					q.Close();
				}
				catch (HttpRequestException hre)
				{
					OdnoException.HandleException(new OdnoException(hre), hre.Message, "ResponseBody.GetDateFromMusicBrainz");
					q.Close();
				}
				catch (TimeoutException te) {
                    OdnoException.HandleException(new OdnoException(te), te.Message, "ResponseBody.GetDateFromMusicBrainz");
                }
                catch
                {
                    OdnoException.HandleException(new OdnoException(new Exception("Could not get date from musicbrainz")), "Failed to get date.", "ResponseBody.GetDateFromMusicBrainz");
                }

                return 0;
            }

			internal async void DownloadImage(string imgUrl, string tmp) {
				if (!("".Equals(imgUrl) || "".Equals(tmp))) {
                    var stream = await _client.GetStreamAsync(imgUrl);
                    string savedImg = "";
                    try
                    {
                        Bitmap bitmap = new Bitmap(stream);

                        savedImg = $"{tmp}\\cover.jpg";

                        if (bitmap != null)
                        {
                            bitmap.Save(savedImg, ImageFormat.Jpeg);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Image could not be downloaded.", "ODNO INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        stream.Flush();
                        stream.Close();
                        _client.Dispose();
                    }
                }
            }
        }

		/// <summary>
		/// Send GET request to target URL.
		/// </summary>
		/// <returns>ResponseBody</returns>
		/// <exception cref="HttpRequestException"></exception>
		public async Task<ResponseBody> get() {
			try
			{
				if (_url.Equals("") || _url == null)
				{
					throw new ArgumentException("ERROR: Invalid URL");
				}

				HttpService httpSrv = new HttpService();
				var resp = await httpSrv.GetAsync(_url);

				_response = await resp.Content.ReadAsStringAsync();

				_isSuccess = resp.IsSuccessStatusCode;

				_responseCode = (int)resp.StatusCode;

				var lastfmJson = await httpSrv.GetJsonResponse();
                if (lastfmJson.error != null) { 
					_isSuccess = false;
					return this;
				}

                int? year = await httpSrv.GetDateFromMusicBrainz(lastfmJson.mbid);

                LastFmObjectModel.Album finalJson = new LastFmObjectModel.Album(
                    lastfmJson.artist, lastfmJson.mbid, lastfmJson.tags,
                    lastfmJson.playcount, lastfmJson.image, lastfmJson.tracks,
                    lastfmJson.url, lastfmJson.name, lastfmJson.listeners, lastfmJson.wiki, "none", year);

				_responseJson = finalJson;

            }
			catch (HttpRequestException hre)
			{
				_isSuccess = false;
				throw;
			}
			catch (ArgumentException ae) {
                _isSuccess = false;
                throw new HttpRequestException($"Failed to get content from source: {_url}.", ae);
            }
            catch (NotSupportedException nse)
            {
                _isSuccess = false;
                throw new HttpRequestException($"Failed to get content from source: {_url}.", nse);
            }

            return this;
        }

		public void reset() {
			_response = "";
			_responseCode= 0;
			_responseJson = null;
			_isSuccess = false;
			_resultList.Clear();
			_url = "";
			//TODO: log errors before clearing
			_errList.Clear();
		}

		//builder
		internal class ResponseBodyBuilder {
			internal string _url;

			internal string? _response = "";
			internal bool _isSuccess;
			internal int _statusCode;
			internal LastFmObjectModel.Album? _responseJson = null;
			internal List<Song> _resultList = new List<Song>();
            internal List<string> _errList = new List<string>();

            public ResponseBodyBuilder(string url) { 
				_url = url;
			}

			public ResponseBodyBuilder Response(string val) { _response = val; return this; }
			public ResponseBodyBuilder IsSuccess(bool val) { _isSuccess = val; return this; }
            public ResponseBodyBuilder StatusCode(int val) { _statusCode = val; return this; }
            public ResponseBodyBuilder ResponseJson(LastFmObjectModel.Album val) { _responseJson = val; return this; }
			public ResponseBodyBuilder ResultList(Song val) { _resultList.Add(val); return this; }
            public ResponseBodyBuilder ErrList(string val) { _errList.Add(val); return this; }

			public ResponseBody build() {
				return new ResponseBody(this);
			}
        }

		private ResponseBody(ResponseBodyBuilder builder) { 
			_url = builder._url;
			_response = builder._response;
			_isSuccess = builder._isSuccess;
			_responseJson = builder._responseJson;
			_errList = builder._errList;
			_resultList = builder._resultList;
		}

		public bool IsSuccess
		{
			get { return _isSuccess; }
			set { _isSuccess = value; }
		}


		public List<string> ErrList
		{
			get { return _errList; }
			set { _errList = value; }
		}


		public List<Song> ResultList
		{
			get { return _resultList; }
			set { _resultList = value; }
		}

		public LastFmObjectModel.Album ResponseJson
		{
			get { return _responseJson; }
			set { _responseJson = value; }
		}

		public string Response
		{
			get { return _response; }
			set { _response = value; }
		}

		public int ResponseCode
		{
			get { return _responseCode; }
			set { _responseCode = value; }
		}

		public string URL
		{
			get { return _url; }
			set { _url = value; }
		}

		override
		public string ToString() {
			return $"response body: [response_code={_responseCode}, response={_response}, err={_errList}";
		}
	}
}
