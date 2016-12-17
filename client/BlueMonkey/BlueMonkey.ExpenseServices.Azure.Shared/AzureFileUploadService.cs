using BlueMonkey.MediaServices;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Threading.Tasks;

namespace BlueMonkey.ExpenseServices.Azure
{
    public class AzureFileUploadService : IFileUploadService
    {
        private static readonly string ContainerName = "pictures";

        private readonly CloudBlobContainer _container;

        public AzureFileUploadService()
        {
            var account = CloudStorageAccount.Parse(Secrets.FileUploadStorageConnectionString);
            var client = account.CreateCloudBlobClient();
            _container = client.GetContainerReference(ContainerName);
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
