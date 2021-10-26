using System.Threading.Tasks;
using Intive.ConfR.Application.Reservations.Commands.CreateRandomReservation;
using Intive.ConfR.Application.Reservations.Commands.CancelReservation;
using Intive.ConfR.Application.Reservations.Commands.CreateReservation;
using Intive.ConfR.Application.Reservations.Commands.DeleteReservation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Intive.ConfR.Application.Reservations.Queries.GetReservation;
using Intive.ConfR.Application.Reservations.Queries.GetAllReservations;

namespace Intive.ConfR.API.Controllers
{
    [Authorize]
    public class ReservationsController : BaseController
    {
        /// <summary>
        /// Get all reservations
        /// </summary>
        /// <param name="query">mail, start</param>
        /// <returns>Reservations list</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Reservation not found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAllReservationsModel))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<GetAllReservationsModel>> GetAll([FromQuery]GetAllReservationsQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        /// <summary>
        /// Get reservation by id
        /// </summary>
        /// <param name="query">Id, Mail</param>
        /// <returns>Reservation</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Reservation not found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet("id")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetReservationModel))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<GetReservationModel>> Get([FromQuery]GetReservationQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        /// <summary>
        /// Creates a new room reservation
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Nothing</returns>
        /// <response code="204">Success</response>
        /// <response code="400">Failure</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Room not found</response>
        /// <response code="409">Room is already reserved for this date</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Create([FromBody]CreateReservationCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Creates a random reservation
        /// </summary>
        /// <param name="command"></param>
        /// <response code="204">Success</response>
        /// <response code="400">Failure</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Room not found</response>
        /// <response code="409">There is no available room at this date</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost("random")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateRandom([FromBody]CreateRandomReservationCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }
        
        /// <summary>
        /// Deletes room reservation
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Nothing</returns>
        /// <response code="204">Success</response>
        /// <response code="400">Failure</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Reservation not found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Delete(string id)
        {
            await Mediator.Send(new DeleteReservationCommand { Id = id } );

            return NoContent();
        }

        /// <summary>
        /// Cancel reservation
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Nothing</returns>
        /// <response code="202">Accepted</response>
        /// <response code="400">Failure</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Reservation not found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete("cancel")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Cancel([FromQuery]CancelReservationCommand command)
        {
            await Mediator.Send(command);

            return Accepted();
        }
    }
}