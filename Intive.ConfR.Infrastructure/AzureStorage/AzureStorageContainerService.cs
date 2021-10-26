using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Intive.ConfR.Application.Interfaces.AzureStorage;
using Intive.ConfR.Domain.Entities;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Intive.ConfR.Infrastructure.AzureStorage
{
    public class AzureStorageContainerService : IAzureStorageContainerService
    {
        private readonly IStorageConnectionHelper _storageConnectionHelper;

        public AzureStorageContainerService(IStorageConnectionHelper storageConnectionHelper)
        {
            _storageConnectionHelper = storageConnectionHelper;
        }

        public async Task CreateContainerAsync(string name, bool makePublic = false)
        {
            var blobContainer = GetBlobContainer(name);

            await blobContainer.CreateAsync();

            if (makePublic)
            {
                var permissions = await blobContainer.GetPermissionsAsync();

                permissions.PublicAccess = BlobContainerPublicAccessType.Container;
                await blobContainer.SetPermissionsAsync(permissions);
            }
        }

        public async Task<IEnumerable<RoomPhoto>> GetContainerContentsAsync(string name, bool requireSas)
        {
            var blobContainer = GetBlobContainer(name);
            var blobs = await blobContainer.ListBlobsSegmentedAsync(new BlobContinuationToken());
            var policy = new SharedAccessBlobPolicy
            {
                Permissions = SharedAccessBlobPermissions.Read | SharedAccessBlobPermissions.List,
                SharedAccessStartTime = DateTimeOffset.Now,
                SharedAccessExpiryTime = DateTimeOffset.Now.AddDays(1)
            };

            var sas = blobContainer.GetSharedAccessSignature(policy);


            var roomPhotos = from blobItem in blobs.Results
                select new RoomPhoto
                {
                    Name = blobItem.Uri.Segments[blobItem.Uri.Segments.Length - 1],
                    Path = blobItem.Uri.ToString(),
                    SharedAccessSignature = requireSas ? sas : null
                };

            return roomPhotos;
        }

        public Task DeleteContainerAsync(string name)
        {
            var blobContainer = GetBlobContainer(name);

            return blobContainer.DeleteAsync();
        }

        public Task<bool> ContainerExists(string name)
        {
            var blobContainer = GetBlobContainer(name);

            return blobContainer.ExistsAsync();
        }

        private CloudBlobContainer GetBlobContainer(string containerName)
        {
            var blobClient = _storageConnectionHelper.GetCloudBlobClient();

            return blobClient.GetContainerReference(containerName);
        }
    }
}
