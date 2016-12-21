using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueMonkey.MediaServices
{
    public static class MediaFileExtensions
    {
        public static async Task<IMediaFile> Convert(this Plugin.Media.Abstractions.MediaFile source)
        {
            if (source != null)
            {
                string extension;
                if (source.Path == null)
                {
                    extension = null;
                }
                else
                {
                    var index = source.Path.LastIndexOf('.');
                    extension = index < 0 ? null : source.Path.Substring(index, source.Path.Length - index);
                }

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
