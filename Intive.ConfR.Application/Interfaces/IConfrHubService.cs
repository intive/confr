using System.Threading.Tasks;
using Intive.ConfR.Domain.Entities;

namespace Intive.ConfR.Application.Interfaces
{
    public interface IConfrHubService
    {
        Task SendNewReservationToClient(GraphReservation response);
        void SendCanceledReservationToClient(GraphReservation response);
        Task SendNewPhotoToClient(string email, string newPhoto, string containerName);
        Task SendUpdatedPhotoToClient(string email, string newPhoto, string oldPhoto, string containerName);
        Task SendRemovedPhotoToClient(string email, string photoName);
    }
}