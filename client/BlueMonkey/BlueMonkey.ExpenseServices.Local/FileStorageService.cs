using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueMonkey.MediaServices;

namespace BlueMonkey.ExpenseServices.Local
{
    public class FileStorageService : IFileStorageService
    {
        public Task<Uri> UploadMediaFileAsync(IMediaFile mediaFile)
        {
            return Task.FromResult(new Uri("test.jpg", UriKind.Relative));
        }
    }
}
