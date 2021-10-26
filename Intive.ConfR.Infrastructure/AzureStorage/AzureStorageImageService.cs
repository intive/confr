using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Dapper;
using Intive.ConfR.Application.Exceptions;
using Intive.ConfR.Application.Interfaces.AzureStorage;
using Intive.ConfR.Domain.Entities;
using Intive.ConfR.Infrastructure.AzureStorage.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage.Blob;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.PixelFormats;

namespace Intive.ConfR.Infrastructure.AzureStorage
{
    //TODO: Code cleanup, create smaller classes, methods - make it more universal, not only for images.
    //TODO: Resolve safety issues. Change CORS settings instead of making container public.
    public class AzureStorageImageService : IAzureStorageImageService
    {
        private readonly IStorageConnectionHelper _storageConnectionHelper;
        private readonly INameGenerator _nameGenerator;
        private readonly IImageProcessingService _imageProcessingService;
        private readonly AcceptedExtensions _acceptedExtensions;

        public AzureStorageImageService(IStorageConnectionHelper storageConnectionHelper, 
            INameGenerator nameGenerator, 
            IImageProcessingService imageProcessingService,
            IOptions<AcceptedExtensions> extensionOptions)
        {
            _storageConnectionHelper = storageConnectionHelper;
            _nameGenerator = nameGenerator;
            _imageProcessingService = imageProcessingService;
            _acceptedExtensions = extensionOptions.Value;
        }

        public async Task<RoomPhoto> FindPhoto(string name, string containerName, bool requireSas)
        {
            var blockBlob = GetBlockBlob(name, containerName);

            if(string.IsNullOrEmpty(blockBlob.Uri.ToString()))
            {
                return null;
            }

            var policy = new SharedAccessBlobPolicy
            {
                Permissions = SharedAccessBlobPermissions.Read,
                SharedAccessStartTime = DateTimeOffset.Now,
                SharedAccessExpiryTime = DateTimeOffset.Now.AddDays(1)
            };

            var sas = blockBlob.GetSharedAccessSignature(policy);

            if (!await blockBlob.ExistsAsync())
            {
                throw new ImageNotFoundException(name, containerName);
            }

            return new RoomPhoto
            {
                Name = blockBlob.Uri.Segments.Last(),
                Path = blockBlob.Uri.ToString(),
                SharedAccessSignature = requireSas ? sas : null
            };
        }

        public Task<bool> PhotoExists(string name, string containerName)
        {
            return GetBlockBlob(name, containerName).ExistsAsync();
        }

        public async Task<string> UploadPhotoFromStream(Stream stream, string containerName, string name, int width, int height)
        {
            CloudBlockBlob blockBlob;
            Image<Rgba32> thumbnail;
            IImageFormat imageFormat;
            string photoName;

            using (var img = Image.Load(stream, out imageFormat))
            {
                if (!_acceptedExtensions.Extensions.AsList().Contains(imageFormat.Name.ToUpper()))
                {
                    throw new ImageFormatException("Bad photo format.");
                }

                photoName = string.IsNullOrEmpty(name) ? _nameGenerator.GeneratePhotoName(imageFormat.Name.ToLower()) : name;

                blockBlob = GetBlockBlob(photoName, containerName);
                blockBlob.Properties.ContentType = imageFormat.DefaultMimeType;

                thumbnail = _imageProcessingService.CreatePhotoThumbnail(img, width, height);
            }

            var photoUri = blockBlob.Uri.AbsoluteUri;

            stream.Seek(0, SeekOrigin.Begin);

            await blockBlob.UploadFromStreamAsync(stream);

            using (var memoryStream = new MemoryStream())
            {
                using (thumbnail)
                {
                    var thumbnailName = _nameGenerator.GenerateThumbnailName(photoName);
                    thumbnail.Save(memoryStream, imageFormat);

                    blockBlob = GetBlockBlob(thumbnailName, containerName);
                    blockBlob.Properties.ContentType = imageFormat.DefaultMimeType;

                    memoryStream.Seek(0, SeekOrigin.Begin);

                    await blockBlob.UploadFromStreamAsync(memoryStream);
                }
            }

            return photoUri;
        }

        public Task<bool> DeletePhotoAsync(string name, string containerName)
        {
            var blockBlob = GetBlockBlob(name, containerName);

            return blockBlob.DeleteIfExistsAsync();
        }

        private CloudBlockBlob GetBlockBlob(string photoName, string containerName)
        {
            var blobClient = _storageConnectionHelper.GetCloudBlobClient();
            var blobContainer = blobClient.GetContainerReference(containerName);

            return blobContainer.GetBlockBlobReference(photoName);
        }
    }
}
