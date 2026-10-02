using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdnoWindowsApp.Model
{
    public class LastFmObjectModel
    {
        public record Album(
            [property: JsonProperty("artist")] string artist,
            [property: JsonProperty("mbid")] string mbid,
            [property: JsonProperty("tags")] Tags tags,
            [property: JsonProperty("playcount")] string playcount,
            [property: JsonProperty("image")] IReadOnlyList<Image> image,
            [property: JsonProperty("tracks")] Tracks tracks,
            [property: JsonProperty("url")] string url,
            [property: JsonProperty("name")] string name,
            [property: JsonProperty("listeners")] string listeners,
            [property: JsonProperty("wiki")] Wiki? wiki,
            [property: JsonProperty("error")] string error,
            [property: JsonProperty("date")] int? year
         );

        public record Artist(
            [property: JsonProperty("url")] string url,
            [property: JsonProperty("name")] string name,
            [property: JsonProperty("mbid")] string mbid
        );

        public record Attr(
            [property: JsonProperty("rank")] int rank
        );

        public record Image(
            [property: JsonProperty("size")] string size,
            [property: JsonProperty("#text")] string text
        );

        public record Root(
            [property: JsonProperty("album")] Album album
        );

        public record Streamable(
            [property: JsonProperty("fulltrack")] string fulltrack,
            [property: JsonProperty("#text")] string text
        );

        public record Tag(
            [property: JsonProperty("url")] string url,
            [property: JsonProperty("name")] string name
        );

        public record Tags(
            [property: JsonProperty("tag")] IReadOnlyList<Tag> tag
        );

        public record Track(
            [property: JsonProperty("streamable")] Streamable streamable,
            [property: JsonProperty("url")] string url,
            [property: JsonProperty("name")] string name,
            [property: JsonProperty("@attr")] Attr attr,
            [property: JsonProperty("artist")] Artist artist
        );

        public record Tracks(
            [property: JsonProperty("track")] IReadOnlyList<Track> track
        );

        public record Wiki(
            [property: JsonProperty("published")] string? published,
            [property: JsonProperty("summary")] string? summary,
            [property: JsonProperty("content")] string? content
        );
    }
}
