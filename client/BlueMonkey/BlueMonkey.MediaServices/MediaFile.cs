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
        public string Path { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="image"></param>
        public MediaFile(string path, byte[] image)
        {
            Path = path;
            _image = image;
        }

        public Stream GetStream()
        {
            return new MemoryStream(_image);
        }
    }
}
