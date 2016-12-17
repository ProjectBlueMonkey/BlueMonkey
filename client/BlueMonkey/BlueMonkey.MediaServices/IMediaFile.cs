using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueMonkey.MediaServices
{
    public interface IMediaFile : IDisposable
    {
        /// <summary>
        /// Path to file.
        /// </summary>
        string Path { get; }
        /// <summary>
        /// Get stream if available
        /// </summary>
        /// <returns></returns>
        Stream GetStream();
    }
}
