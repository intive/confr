using Intive.ConfR.Domain.Entities;
using Intive.ConfR.Application.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Intive.ConfR.Infrastructure.ApiClient;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using Intive.ConfR.Domain;
using Intive.ConfR.Domain.ValueObjects;
using Intive.ConfR.Application.Exceptions;

namespace Intive.ConfR.Infrastructure
{
    public class MicrosoftGraphRoomsApi : IRoomsDirectory
    {
        private readonly MicrosoftGraphApiClient _apiClient = new MicrosoftGraphApiClient();
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        private readonly DefaultHttpContext _httpContext = new DefaultHttpContext();

        public MicrosoftGraphRoomsApi(IMapper mapper, IAuthService authService)
        {
            _mapper = mapper;
            _authService = authService;
        }

        public async Task<IList<Room>> FindRooms()
        {
            _httpContext.Request.Headers["Access_token"] = await _authService.GetAccessToken();

            var request = new GraphApiGetRequest
            {
                Endpoint = "/me/findRooms",
                GraphVersion = "beta"
            };

            var roomDTOs = await _apiClient.Get<GraphListResponse<RoomDTO>>(request, _httpContext);

            var rooms = _mapper.Map<IList<RoomDTO>, IList<Room>>(roomDTOs.Value);

            return rooms;

        }

        public async Task<Room> FindRoomByEmail(string email)
        {
            _httpContext.Request.Headers["Access_token"] = await _authService.GetAccessToken();

            var request = new GraphApiGetRequest
            {
                GraphVersion = "beta/",
                Endpoint = $"users/{email}"
            };

            var graphRoom = await _apiClient.Get<GraphDetailedRoom>(request, _httpContext);

            var room = _mapper.Map<GraphDetailedRoom, Room>(graphRoom);

            return room;
        }

        public async Task<bool> RoomExists(EMailAddress email)
        {
            var emailToString = _mapper.Map<string>(email);
            try
            {
                await FindRoomByEmail(emailToString);
                return true;
            }
            catch (GraphApiException e)
            {
                if (e.StatusCode == 404)
                {
                    return false;
                }

                throw;
            }

        }
    }
}
