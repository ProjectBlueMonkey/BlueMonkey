using System;
using System.Threading.Tasks;
using BlueMonkey.MediaServices;

namespace BlueMonkey.ExpenseServices
{
    public interface IFileStorageService
    {
        Task<Uri> UploadMediaFileAsync(IMediaFile mediaFile);

        Task<IMediaFile> DownloadMediaFileAsync(Uri uri);
    }
}
