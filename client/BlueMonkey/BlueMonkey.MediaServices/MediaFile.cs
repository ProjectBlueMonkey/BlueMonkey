using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueMonkey.MediaServices
{
    public class MediaFile : IMediaFile
    {
        private readonly byte[] _image;
        public string Extension { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="extension"></param>
        /// <param name="image"></param>
        public MediaFile(string extension, byte[] image)
        {
            Extension = extension;
            _image = image;
        }

        public Stream GetStream()
        {
            return new MemoryStream(_image);
        }
    }
}
