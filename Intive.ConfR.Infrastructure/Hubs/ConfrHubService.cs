using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Intive.ConfR.Application.Interfaces;
using Intive.ConfR.Application.Interfaces.AzureStorage;
using Intive.ConfR.Application.Photos.Models;
using Intive.ConfR.Domain.Entities;
using Microsoft.AspNetCore.SignalR;

namespace Intive.ConfR.Infrastructure.Hubs
{
    public class ConfrHubService : IConfrHubService
    {
        private readonly IHubContext<ConfrHub, IHubClient> _hubContext;
        private readonly IAzureStorageImageService _imageService;
        private readonly IReservationService _reservationService;

        public ConfrHubService(IHubContext<ConfrHub, IHubClient> hubContext, IAzureStorageImageService imageService, IReservationService reservationService)
        {
            _hubContext = hubContext;
            _imageService = imageService;
            _reservationService = reservationService;
        }

        /// <summary>
        /// Sends information about new reservation to client hub
        /// </summary>
        /// <param name="response"><see cref="GraphReservation"/></param>
        public async Task SendNewReservationToClient(GraphReservation response)
        {
            var userReservation = GetReservationData(response);
            var roomsEmails = GetEmails(response);

            //TODO: Fix delay for room reservation after creating new reservation
            await Task.Delay(1000);

            roomsEmails.ForEach(async email =>
            {
                var list = await _reservationService.GetReservationsList(userReservation.StartTime, userReservation.EndTime, email);
                var reservation = GetReservationData(list.First());

                await _hubContext.Clients.All.AddedRoomReservation(email, reservation);
            });
        }

        /// <summary>
        /// Sends information about canceled reservation to client hub
        /// </summary>
        /// <param name="response"><see cref="GraphReservation"/></param>
        public void SendCanceledReservationToClient(GraphReservation response)
        {
            var reservation = GetReservationData(response);
            var roomsEmails = GetEmails(response);

            roomsEmails.ForEach(async email =>
                await _hubContext.Clients.All.CanceledRoomReservation(email, reservation));
        }

        /// <summary>
        /// Sends information about added photo to client hub
        /// </summary>
        /// <param name="email">Room email</param>
        /// <param name="newPhoto">Name of added photo</param>
        public async Task SendNewPhotoToClient(string email, string newPhoto, string containerName)
        {
            var photo = await _imageService.FindPhoto(newPhoto, containerName, true);

            var photoModel = new RoomPhotoDto
            {
                Name = photo.Name,
                Path = string.Concat(photo.Path, photo.SharedAccessSignature)
            };

            await _hubContext.Clients.All.AddedRoomPhoto(email, photoModel);
        }

        /// <summary>
        /// Sends information about updated photo to client hub
        /// </summary>
        /// <param name="email">Room email</param>
        /// <param name="newPhoto">Name of updated photo</param>
        /// <param name="oldPhoto">Old photo name</param>
        /// <param name="containerName">Container name</param>
        public async Task SendUpdatedPhotoToClient(string email, string newPhoto, string oldPhoto, string containerName)
        {
            var photo = await _imageService.FindPhoto(newPhoto, containerName, true);

            var photoModel = new RoomPhotoDto
            {
                Name = photo.Name,
                Path = string.Concat(photo.Path, photo.SharedAccessSignature)
            };

            await _hubContext.Clients.All.UpdatedRoomPhoto(email, oldPhoto, photoModel);
        }

        /// <summary>
        /// Sends information about removed photo to client hub
        /// </summary>
        /// <param name="email">Room email</param>
        /// <param name="photoName">Photo name</param>
        public async Task SendRemovedPhotoToClient(string email, string photoName)
        {
            await _hubContext.Clients.All.RemovedRoomPhoto(email, photoName);
        }

        /// <summary>
        /// Returns list of rooms emails from Microsoft Graph response
        /// </summary>
        /// <param name="response"></param>
        /// <returns>List of emails</returns>
        private static List<string> GetEmails(GraphReservation response)
        {
            return response.Attendees
                .Where(a => a.Type == "resource")
                .Select(a => a.EmailAddress.Address)
                .ToList();
        }

        /// <summary>
        /// Returns information about reservation from Microsoft Graph response
        /// </summary>
        /// <param name="response"></param>
        /// <returns><see cref="Reservation"/></returns>
        private static Reservation GetReservationData(GraphReservation response)
        {
            return new Reservation
            {
                Id = response.Id,
                Subject = response.Subject,
                StartTime = response.Start.DateTime,
                EndTime = response.End.DateTime,
                Owner =  response.Organizer.EmailAddress.Address
            };
        }
    }
}
