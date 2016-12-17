using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueMonkey.MediaServices
{
    public interface IMediaService
    {
        /// <summary>
        /// Initialize all camera components
        /// </summary>
        /// <returns></returns>
        Task<bool> InitializeAsync();
        /// <summary>
        /// Gets if a camera is available on the device
        /// </summary>
        bool IsCameraAvailable { get; }
        /// <summary>
        /// Gets if ability to take photos supported on the device
        /// </summary>
        bool IsTakePhotoSupported { get; }

        /// <summary>
        /// Gets if the ability to pick photo is supported on the device
        /// </summary>
        bool IsPickPhotoSupported { get; }
        /// <summary>
        /// Picks a photo from the default gallery
        /// </summary>
        /// <returns>Media file or null if canceled</returns>
        Task<IMediaFile> PickPhotoAsync(PickMediaOptions options = null);

        /// <summary>
        /// Take a photo async with specified options
        /// </summary>
        /// <param name="options">Camera Media Options</param>
        /// <returns>Media file of photo or null if canceled</returns>
        Task<IMediaFile> TakePhotoAsync(StoreCameraMediaOptions options);
    }
}
