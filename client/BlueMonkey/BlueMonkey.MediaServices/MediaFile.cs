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
        public string Path { get; }
        public Stream GetStream()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
