using System.Threading.Tasks;
using Intive.ConfR.Application.Photos.Models;
using Intive.ConfR.Application.Rooms.Queries.GetRoomDetail;
using Intive.ConfR.Application.Rooms.Queries.GetRoomsList;
using Intive.ConfR.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Intive.ConfR.Infrastructure.Hubs
{
    public interface IHubClient
    {
        Task ReceivedRoomBasics(RoomLookupModel room);
        Task UpdatedRoomDetails(GetRoomDetailModel room);
        Task RemovedRoom(string roomEmail);

        Task AddedRoomReservation(string roomEmail, Reservation reservation);
        Task UpdatedRoomReservation(string roomEmail, string oldReservationId, Reservation newReservation);
        Task CanceledRoomReservation(string roomEmail, Reservation reservation);

        Task AddedRoomPhoto(string roomEmail, RoomPhotoDto newPhoto);
        Task UpdatedRoomPhoto(string roomEmail, string oldPhotoName, RoomPhotoDto newPhoto);
        Task RemovedRoomPhoto(string roomEmail, string photoName);
    }

    [Authorize]
    public class ConfrHub : Hub<IHubClient>
    {}
}
