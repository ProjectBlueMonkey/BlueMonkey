using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueMonkey.MediaServices;

namespace BlueMonkey.FileUploadServices
{
    public interface IFileUploadService
    {
        Task<Uri> UploadMediaFileAsync(IMediaFile mediaFile);
    }
}
