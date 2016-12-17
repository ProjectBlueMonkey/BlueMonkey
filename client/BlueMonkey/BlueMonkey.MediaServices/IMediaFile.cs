using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueMonkey.MediaServices
{
    public interface IMediaFile
    {
        /// <summary>
        /// Extension of media file. Start with a period.
        /// </summary>
        string Extension { get; }
        /// <summary>
        /// Get stream if available
        /// </summary>
        /// <returns></returns>
        Stream GetStream();
    }
}
