using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BlueMonkey.MediaServices.Tests
{
    public class MediaServiceExtensionsTest
    {
        [Fact]
        public async Task Convert()
        {
            var bytes = new byte[] {1};
            var media = new Plugin.Media.Abstractions.MediaFile("/hoge/foo/bar/image.jpg", () => new MemoryStream(bytes));

            var actual = await media.Convert();
            Assert.NotNull(actual);
            Assert.Equal(".jpg", actual.Extension);

            using (var sourceStream = actual.GetStream())
            using (var memoryStream = new MemoryStream())
            {
                Assert.NotNull(sourceStream);
                sourceStream.CopyTo(memoryStream);

                var actualArray = memoryStream.ToArray();
                Assert.Single(actualArray);
                Assert.Equal(bytes[0], actualArray[0]);
            }
        }

        [Fact]
        public async Task ConvertWhenPathIsNull()
        {
            var bytes = new byte[] { 1 };
            var media = new Plugin.Media.Abstractions.MediaFile(null, () => new MemoryStream(bytes));

            var actual = await media.Convert();
            Assert.NotNull(actual);
            Assert.Null(actual.Extension);
        }

        [Fact]
        public async Task ConvertWhenNotExistExtension()
        {
            var bytes = new byte[] { 1 };
            var media = new Plugin.Media.Abstractions.MediaFile("/hoge/foo/bar/image", () => new MemoryStream(bytes));

            var actual = await media.Convert();
            Assert.NotNull(actual);
            Assert.Null(actual.Extension);
        }
    }
}
