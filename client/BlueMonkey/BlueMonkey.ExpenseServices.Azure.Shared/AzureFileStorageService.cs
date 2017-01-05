using BlueMonkey.MediaServices;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BlueMonkey.ExpenseServices.Azure
{
    public class AzureFileStorageService : IFileStorageService
    {
        private static readonly string ContainerName = "pictures";

        private readonly CloudBlobContainer _container;

        public AzureFileStorageService()
        {
            var account = CloudStorageAccount.Parse(Secrets.FileUploadStorageConnectionString);
            var client = account.CreateCloudBlobClient();
            _container = client.GetContainerReference(ContainerName);
        }

        public async Task<IMediaFile> DownloadMediaFileAsync(Uri uri)
        {
            await _container.CreateIfNotExistsAsync();
            await _container.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            var fileName = Path.GetFileName(uri.AbsolutePath);
            var blockBlob = _container.GetBlockBlobReference(fileName);
            using (var ms = new MemoryStream())
            {
                await blockBlob.DownloadToStreamAsync(ms);
                ms.Seek(0, SeekOrigin.Begin);
                return new MediaFile(Path.GetExtension(fileName), ms.ToArray());
            }
        }

        public async Task<Uri> UploadMediaFileAsync(IMediaFile mediaFile)
        {
            await _container.CreateIfNotExistsAsync();
            await _container.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            var fileName = $"{Guid.NewGuid()}{mediaFile.Extension}";
            var blockBlob = _container.GetBlockBlobReference(fileName);

            using (var stream = mediaFile.GetStream())
            {
                await blockBlob.UploadFromStreamAsync(stream);
            }

            return blockBlob.Uri;
        }
    }
}
