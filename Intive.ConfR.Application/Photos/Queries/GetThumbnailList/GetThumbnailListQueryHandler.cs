using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Intive.ConfR.Application.Exceptions;
using Intive.ConfR.Application.Interfaces.AzureStorage;
using Intive.ConfR.Application.Photos.Models;
using MediatR;

namespace Intive.ConfR.Application.Photos.Queries.GetThumbnailList
{
    public class GetThumbnailListQueryHandler : IRequestHandler<GetThumbnailListQuery, List<RoomPhotoDto>>
    {
        private readonly IAzureStorageContainerService _azureStorageContainerService;
        private readonly INameGenerator _nameGenerator;
        private readonly IMapper _mapper;

        public GetThumbnailListQueryHandler(IAzureStorageContainerService azureStorageContainerService, 
            INameGenerator nameGenerator, 
            IMapper mapper)
        {
            _azureStorageContainerService = azureStorageContainerService;
            _nameGenerator = nameGenerator;
            _mapper = mapper;
        }

        public async Task<List<RoomPhotoDto>> Handle(GetThumbnailListQuery request, CancellationToken cancellationToken)
        {
            var containerName = _nameGenerator.GenerateContainerName(request.Email);

            if (!await _azureStorageContainerService.ContainerExists(containerName))
            {
                throw new ContainerNotFoundException(containerName);
            }

            var photos = _azureStorageContainerService.GetContainerContentsAsync(containerName, request.RequireSas).Result;

            photos = photos.Where(photo => photo.Name.Contains("thumbnail"));

            var result = _mapper.Map<List<RoomPhotoDto>>(photos);

            return result;
        }
    }
}
