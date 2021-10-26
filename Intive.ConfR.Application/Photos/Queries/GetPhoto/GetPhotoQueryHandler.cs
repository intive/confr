using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Intive.ConfR.Application.Exceptions;
using Intive.ConfR.Application.Interfaces.AzureStorage;
using Intive.ConfR.Application.Photos.Models;
using MediatR;

namespace Intive.ConfR.Application.Photos.Queries.GetPhoto
{
    public class GetPhotoQueryHandler : IRequestHandler<GetPhotoQuery, RoomPhotoDto>
    {
        private readonly IAzureStorageContainerService _azureStorageContainerService;
        private readonly IAzureStorageImageService _azureStorageImageService;
        private readonly INameGenerator _nameGenerator;
        private readonly IMapper _mapper;

        public GetPhotoQueryHandler(IAzureStorageContainerService azureStorageContainerService,
            IAzureStorageImageService azureStorageImageService,
            INameGenerator nameGenerator,
            IMapper mapper)
        {
            _azureStorageContainerService = azureStorageContainerService;
            _azureStorageImageService = azureStorageImageService;
            _nameGenerator = nameGenerator;
            _mapper = mapper;
        }

        public async Task<RoomPhotoDto> Handle(GetPhotoQuery request, CancellationToken cancellationToken)
        {
            var containerName = _nameGenerator.GenerateContainerName(request.Email);

            if (!await _azureStorageContainerService.ContainerExists(containerName))
            {
                throw new ContainerNotFoundException(containerName);
            }

            var photo = await _azureStorageImageService.FindPhoto(request.Name,
                _nameGenerator.GenerateContainerName(request.Email), request.RequireSas);
            
            var result = _mapper.Map<RoomPhotoDto>(photo);

            return result;
        }
    }
}
