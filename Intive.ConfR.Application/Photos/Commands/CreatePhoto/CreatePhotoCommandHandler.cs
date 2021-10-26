using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Intive.ConfR.Application.Interfaces;
using Intive.ConfR.Application.Interfaces.AzureStorage;
using MediatR;

namespace Intive.ConfR.Application.Photos.Commands.CreatePhoto
{
    public class CreatePhotoCommandHandler : IRequestHandler<CreatePhotoCommand, string>
    {
        private readonly IAzureStorageContainerService _azureStorageContainerService;
        private readonly IAzureStorageImageService _azureStorageImageService;
        private readonly INameGenerator _nameGenerator;
        private readonly IConfrHubService _hubService;

        public CreatePhotoCommandHandler(IAzureStorageContainerService azureStorageContainerService, 
            IAzureStorageImageService azureStorageImageService,
            INameGenerator nameGenerator,
            IConfrHubService hubService)
        {
            _azureStorageContainerService = azureStorageContainerService;
            _azureStorageImageService = azureStorageImageService;
            _nameGenerator = nameGenerator;
            _hubService = hubService;
        }

        public async Task<string> Handle(CreatePhotoCommand request, CancellationToken cancellationToken)
        {
            var containerName = _nameGenerator.GenerateContainerName(request.RoomEmail);
            string photoUri;

            if (!await _azureStorageContainerService.ContainerExists(containerName))
            {
                await _azureStorageContainerService.CreateContainerAsync(containerName, true);
            }
            
            using (var photoStream = request.Photo.OpenReadStream())
            {
                photoUri = await _azureStorageImageService.UploadPhotoFromStream(photoStream, containerName, width: request.ThumbnailWidth, height: request.ThumbnailHeight);
            }

            await _hubService.SendNewPhotoToClient(request.RoomEmail, Uri.UnescapeDataString(new Uri(photoUri).Segments.Last()), containerName);

            return photoUri;
        }
    }
}
