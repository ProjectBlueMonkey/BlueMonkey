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
                using (var mediaStream = source.GetStream())
                using (var memoryStream = new MemoryStream())
                {
                    await mediaStream.CopyToAsync(memoryStream);

                    string extension;
                    if (source.Path == null)
                    {
                        extension = null;
                    }
                    else
                    {
                        int index = source.Path.LastIndexOf('.');
                        if (index < 0)
                        {
                            extension = null;
                        }
                        else
                        {
                            extension = source.Path.Substring(index, source.Path.Length - index);
                        }
                    }

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
