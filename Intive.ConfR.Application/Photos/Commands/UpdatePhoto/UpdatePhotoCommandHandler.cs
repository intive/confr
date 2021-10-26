using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Intive.ConfR.Application.Exceptions;
using Intive.ConfR.Application.Interfaces;
using Intive.ConfR.Application.Interfaces.AzureStorage;
using MediatR;

namespace Intive.ConfR.Application.Photos.Commands.UpdatePhoto
{
    public class UpdatePhotoCommandHandler : IRequestHandler<UpdatePhotoCommand>
    {
        private readonly IAzureStorageContainerService _azureStorageContainerService;
        private readonly IAzureStorageImageService _azureStorageImageService;
        private readonly INameGenerator _nameGenerator;
        private readonly IConfrHubService _hubService;

        public UpdatePhotoCommandHandler(IAzureStorageContainerService azureStorageContainerService,
            IAzureStorageImageService azureStorageImageService,
            INameGenerator nameGenerator,
            IConfrHubService hubService)
        {
            _azureStorageContainerService = azureStorageContainerService;
            _azureStorageImageService = azureStorageImageService;
            _nameGenerator = nameGenerator;
            _hubService = hubService;
        }

        public async Task<Unit> Handle(UpdatePhotoCommand request, CancellationToken cancellationToken)
        {
            var containerName = _nameGenerator.GenerateContainerName(request.RoomEmail);
            string photoUri;

            if (!await _azureStorageContainerService.ContainerExists(containerName))
            {
                throw new ContainerNotFoundException(containerName);
            }

            if (!await _azureStorageImageService.PhotoExists(request.PhotoName, containerName))
            {
                throw new ImageNotFoundException(request.PhotoName, containerName);
            }

            using (var photoStream = request.Photo.OpenReadStream())
            {
                photoUri = await _azureStorageImageService.UploadPhotoFromStream(photoStream, containerName, request.PhotoName, request.ThumbnailWidth, request.ThumbnailHeight);
            }

            await _hubService.SendUpdatedPhotoToClient(request.RoomEmail, Uri.UnescapeDataString(new Uri(photoUri).Segments.Last()), request.PhotoName, containerName);

            return Unit.Value;
        }
    }
}
