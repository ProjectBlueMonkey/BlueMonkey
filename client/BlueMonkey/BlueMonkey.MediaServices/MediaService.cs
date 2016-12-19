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
    /// <summary>
    /// A service that provides use of media.
    /// </summary>
    public class MediaService : IMediaService
    {
        /// <summary>
        /// Initialize service.
        /// </summary>
        /// <returns></returns>
        public Task<bool> InitializeAsync()
        {
            return CrossMedia.Current.Initialize();
        }

        /// <summary>
        /// Gets if a camera is available on the device.
        /// </summary>
        public bool IsCameraAvailable => CrossMedia.Current.IsCameraAvailable;
        /// <summary>
        /// Gets if ability to take photos supported on the device.
        /// </summary>
        public bool IsTakePhotoSupported => CrossMedia.Current.IsTakePhotoSupported;

        /// <summary>
        /// Gets if the ability to pick photo is supported on the device.
        /// </summary>
        public bool IsPickPhotoSupported => CrossMedia.Current.IsPickPhotoSupported;

        /// <summary>
        /// Picks a photo from the default gallery.
        /// </summary>
        /// <returns>Media file or null if canceled.</returns>
        public async Task<IMediaFile> PickPhotoAsync()
        {
            var mediaFile = await CrossMedia.Current.PickPhotoAsync();
            return await CreateMediaFile(mediaFile);
        }

        /// <summary>
        /// Take a photo async.
        /// </summary>
        /// <returns>Media file of photo or null if canceled</returns>
        public async Task<IMediaFile> TakePhotoAsync()
        {
            var option = new StoreCameraMediaOptions();
            var mediaFile = await CrossMedia.Current.TakePhotoAsync(option);
            return await CreateMediaFile(mediaFile);
        }

        /// <summary>
        /// Create IMediaFile from Plugin.Media.Abstractions.MediaFile.
        /// </summary>
        /// <param name="mediaFile"></param>
        /// <returns></returns>
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
