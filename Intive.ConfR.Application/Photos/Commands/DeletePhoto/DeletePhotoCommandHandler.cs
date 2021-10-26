using System.Threading;
using System.Threading.Tasks;
using Intive.ConfR.Application.Exceptions;
using Intive.ConfR.Application.Interfaces;
using Intive.ConfR.Application.Interfaces.AzureStorage;
using MediatR;

namespace Intive.ConfR.Application.Photos.Commands.DeletePhoto
{
    public class DeletePhotoCommandHandler : IRequestHandler<DeletePhotoCommand>
    {
        private readonly IAzureStorageContainerService _azureStorageContainerService;
        private readonly IAzureStorageImageService _azureStorageImageService;
        private readonly INameGenerator _nameGenerator;
        private readonly IConfrHubService _hubService;

        public DeletePhotoCommandHandler(IAzureStorageContainerService azureStorageContainerService, 
            IAzureStorageImageService azureStorageImageService, 
            INameGenerator nameGenerator,
            IConfrHubService hubService)
        {
            _azureStorageContainerService = azureStorageContainerService;
            _azureStorageImageService = azureStorageImageService;
            _nameGenerator = nameGenerator;
            _hubService = hubService;
        }

        public async Task<Unit> Handle(DeletePhotoCommand request, CancellationToken cancellationToken)
        {
            var containerName = _nameGenerator.GenerateContainerName(request.RoomEmail);

            if (!await _azureStorageContainerService.ContainerExists(containerName))
            {
                throw new ContainerNotFoundException(containerName);
            }

            if (!await _azureStorageImageService.DeletePhotoAsync(request.PhotoName, containerName))
            {
                throw new ImageNotFoundException(request.PhotoName, containerName);
            }

            await _azureStorageImageService.DeletePhotoAsync(request.PhotoName, containerName);
            await _azureStorageImageService.DeletePhotoAsync(_nameGenerator.GenerateThumbnailName(request.PhotoName), containerName);

            await _hubService.SendRemovedPhotoToClient(request.RoomEmail, request.PhotoName);

            return Unit.Value;
        }
    }
}
