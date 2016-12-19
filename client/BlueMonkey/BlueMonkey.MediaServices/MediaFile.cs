using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueMonkey.MediaServices
{
    /// <summary>
    /// MediaFile class.
    /// </summary>
    public class MediaFile : IMediaFile
    {
        /// <summary>
        /// Byte array of photo.
        /// </summary>
        private readonly byte[] _image;
        /// <summary>
        /// Extension of image file.
        /// </summary>
        public string Extension { get; }
        /// <summary>
        /// Initialize instance.
        /// </summary>
        /// <param name="extension"></param>
        /// <param name="image"></param>
        public MediaFile(string extension, byte[] image)
        {
            Extension = extension;
            _image = image;
        }
        /// <summary>
        /// Get stream of photo.
        /// </summary>
        /// <returns></returns>
        public Stream GetStream()
        {
            return new MemoryStream(_image);
        }
    }
}
