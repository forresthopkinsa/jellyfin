using System;
using MediaBrowser.Model.Net;
using Xunit;

namespace Jellyfin.Model.Tests.Net
{
    public class MimeTypesTests
    {
        [Theory]
        [InlineData("one.jpg", "image/jpeg")]
        [InlineData("two.jpeg", "image/jpeg", false)]
        [InlineData("three.mp4", "video/mp4")]
        [InlineData("four.mkv", "video/x-matroska")]
        [InlineData("five.svg", "image/svg+xml")]
        [InlineData("six-hyphenated.srt", "application/x-subrip")]
        [InlineData("seven.twodots.opus", "audio/ogg", false)]
        [InlineData("eight.three.dots.ass", "text/x-ssa")]
        [InlineData("nine_(\"5P3C!@L~ch&r*c#er$\": {[<?;.',>]} ).ssa", "text/x-ssa")]
        [InlineData("ten.divx", "video/divx")]
        [InlineData("11.htm", "text/html")]
        [InlineData("12.html", "text/html", false)]
        [InlineData("13.log", "text/plain")]
        [InlineData("14.dll", "application/octet-stream", false)]
        [InlineData("15.FakeExtension", "application/octet-stream")]
        [InlineData("16.NoDefault", null, false)]
        public void GetMimeType_Valid(string path, string type, bool enableStreamDefault = true)
        {
            if (enableStreamDefault)
            {
                Assert.Equal(type, MimeTypes.GetMimeType(path));
            }

            Assert.Equal(type, MimeTypes.GetMimeType(path, enableStreamDefault));
        }

        [Fact]
        public void GetMimeType_Error()
        {
            Assert.Throws<ArgumentException>(() => MimeTypes.GetMimeType(string.Empty));
        }

        [Theory]
        [InlineData("image/jpeg", ".jpg")]
        [InlineData("image/jpg", ".jpg")]
        [InlineData("image/png", ".png")]
        [InlineData("image/x-png", ".png")]
        [InlineData("video/mp4", ".mp4")]
        [InlineData("video/vnd.mpeg.dash.mpd", ".mpd")]
        [InlineData("image/svg+xml", ".svg")]
        [InlineData("application/x-subrip", ".srt")]
        [InlineData("audio/aac", ".aac")]
        [InlineData("audio/x-aac", ".aac")]
        [InlineData("audio/ogg", ".oga")]
        [InlineData("text/x-ssa", ".ass")]
        [InlineData("text/x-ssa; charset=UTF-8", ".ass")]
        [InlineData("text/plain", ".edl")]
        [InlineData("text/html", null)]
        [InlineData("text/html; charset=UTF-8", null)]
        [InlineData("image/jpeg: other data", null)]
        [InlineData("image/jpg image/png", null)]
        [InlineData("abc/def", null)]
        public void ToExtension_Valid(string type, string extension)
        {
            Assert.Equal(extension, MimeTypes.ToExtension(type));
        }

        [Fact]
        public void ToExtension_Error()
        {
            Assert.Throws<ArgumentException>(() => MimeTypes.ToExtension(string.Empty));
        }
    }
}
