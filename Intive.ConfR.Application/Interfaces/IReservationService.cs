using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Intive.ConfR.Domain.Entities;

namespace Intive.ConfR.Application.Interfaces
{
    public interface IReservationService
    {
        Task<List<GraphReservation>> GetAllReservations(string mail, string start, int? count);
        Task<List<GraphReservation>> GetReservationsList(DateTime from, DateTime to, string email = null);
        Task<GraphReservation> GetReservation(string id, string mail);
        Task<GraphReservation> CreateReservation(ReservationRequest body);
        Task DeleteReservation(string id);
        Task CancelReservation(string id, string comment);
        List<GraphReservation> SwapReservations(List<GraphReservation> userReservations, List<GraphReservation> roomReservations);
    }
}
