using System.IO;
using System.Threading.Tasks;

namespace BlueMonkey.MediaServices
{
    public static class MediaFileExtensions
    {
        public static async Task<IMediaFile> Convert(this Plugin.Media.Abstractions.MediaFile source)
        {
            if (source != null)
            {
                var extension = Path.GetExtension(source.Path);
                extension = string.IsNullOrEmpty(extension) ? null : extension;

                using (var mediaStream = source.GetStream())
                using (var memoryStream = new MemoryStream())
                {
                    await mediaStream.CopyToAsync(memoryStream);
                    return new MediaFile(extension, memoryStream.ToArray());
                }
            }
            else
            {
                return null;
            }
        }
    }
}
