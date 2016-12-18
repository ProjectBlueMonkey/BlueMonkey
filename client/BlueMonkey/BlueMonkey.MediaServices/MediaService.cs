using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;

namespace BlueMonkey.MediaServices
{
    public class MediaService : IMediaService
    {
        public Task<bool> InitializeAsync()
        {
            return CrossMedia.Current.Initialize();
        }

        public bool IsCameraAvailable => CrossMedia.Current.IsCameraAvailable;
        public bool IsTakePhotoSupported => CrossMedia.Current.IsTakePhotoSupported;
        public bool IsPickPhotoSupported => CrossMedia.Current.IsPickPhotoSupported;
        public async Task<IMediaFile> PickPhotoAsync()
        {
            var mediaFile = await CrossMedia.Current.PickPhotoAsync();
            return await CreateMediaFile(mediaFile);
        }

        public async Task<IMediaFile> TakePhotoAsync()
        {
            var option = new StoreCameraMediaOptions();
            var mediaFile = await CrossMedia.Current.TakePhotoAsync(option);
            return await CreateMediaFile(mediaFile);
        }

        private static async Task<IMediaFile> CreateMediaFile(Plugin.Media.Abstractions.MediaFile mediaFile)
        {
            if (mediaFile != null)
            {
                using (var mediaStream = mediaFile.GetStream())
                using (var memoryStream = new MemoryStream())
                {
                    await mediaStream.CopyToAsync(memoryStream);
                    return new MediaFile(mediaFile.Path, memoryStream.ToArray());
                }
            }
            else
            {
                return null;
            }
        }
    }
}
