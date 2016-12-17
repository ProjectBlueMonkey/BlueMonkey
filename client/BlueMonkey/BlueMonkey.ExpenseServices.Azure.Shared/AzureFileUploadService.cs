using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BlueMonkey.ExpenseServices;
using BlueMonkey.MediaServices;

namespace BlueMonkey.ExpenseServices.Azure
{
    public class AzureFileUploadService : IFileUploadService
    {
        public Task<Uri> UploadMediaFileAsync(IMediaFile mediaFile)
        {
            throw new NotImplementedException();
        }
    }
}
