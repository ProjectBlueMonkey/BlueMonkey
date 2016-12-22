using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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

        public async Task<IMediaFile> DownloadMediaFileAsync(Uri uri)
        {
            var assembly = typeof(FileStorageService).GetTypeInfo().Assembly;
            using (var inputStream = assembly.GetManifestResourceStream("BlueMonkey.ExpenseServices.Local.lena.jpg"))
            using (var outputStream = new MemoryStream())
            {
                await inputStream.CopyToAsync(outputStream);
                return new MediaFile(".jpg", outputStream.ToArray());
            }
        }
    }
}
