using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Intive.ConfR.Application.Rooms.Queries.GetRoomsList;
using Intive.ConfR.Application.Rooms.Queries.GetRoomDetail;
using Intive.ConfR.Application.Rooms.Commands.UpdateRoom;
using Intive.ConfR.Application.Rooms.Commands.CreateRoom;
using Intive.ConfR.Application.Rooms.Commands.DeleteRoom;
using Microsoft.AspNetCore.Http;
using Intive.ConfR.Application.Rooms.Queries.GetRoomReservations;
using System;
using Microsoft.AspNetCore.Authorization;
using Intive.ConfR.Application.Rooms.Queries.GetRoomAvailability;

namespace Intive.ConfR.API.Controllers
{
    [Authorize]
    public class RoomsController : BaseController
    {
        /// <summary>
        /// Displays all rooms
        /// </summary>
        /// <returns>List of existing rooms</returns>
        ///<response code="200">Success</response>
        ///<response code="400">Failure</response>
        ///<response code="401">Unauthorized</response>
        ///<response code="500">Internal Server Error</response>
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(RoomsListViewModel))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<RoomsListViewModel>> GetAll()
        {
            return Ok(await Mediator.Send(new GetRoomsListQuery()));
        }

        /// <summary>
        /// Displays room with the given email
        /// </summary>
        /// <param name="email">It must define the existing room</param>
        /// <returns>Details of the given room</returns>
        /// <response code="200">Success</response>
        ///<response code="400">The room with the given id does not exist</response>
        ///<response code="401">Unauthorized</response>
        ///<response code="404">Room not found</response>
        ///<response code="500">Internal Server Error</response>
        [AllowAnonymous]
        [HttpGet("{email}")]
        [ProducesResponseType(200, Type = typeof(GetRoomDetailModel))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Get(string email)
        {
            return Ok(await Mediator.Send(new GetRoomDetailQuery {Email = email}));
        }

        /// <summary>
        /// Displays a reservation list for a given room within a given time period
        /// </summary>
        /// <param name="email"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns>List of reservations for a given room</returns>
        ///<response code="200">Success</response>
        ///<response code="400">Failure</response>
        ///<response code="401">Unauthorized</response>
        ///<response code="404">Room not found</response>
        ///<response code="500">Internal Server Error</response>
        [HttpGet("{email}/reservations")]
        [ProducesResponseType(200, Type = typeof(RoomReservationsViewModel))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<RoomReservationsViewModel>> GetReservations(string email, [FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            return Ok(await Mediator.Send(new GetRoomReservationsQuery {Email = email, From = from, To = to}));
        }

        /// <summary>
        /// Returns information if a given room is available for a given time period
        /// </summary>
        /// <param name="email"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="availabilityViewInterval"></param>
        /// <returns>Availability status for a given room</returns>
        ///<response code="200">Success</response>
        ///<response code="400">Failure</response>
        ///<response code="401">Unauthorized</response>
        ///<response code="404">Room not found</response>
        ///<response code="500">Internal Server Error</response>
        [HttpGet("{email}/availability")]
        [ProducesResponseType(200, Type = typeof(RoomAvailabilityViewModel))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<RoomAvailabilityViewModel>> GetAvailability(string email, [FromQuery] DateTime from, [FromQuery] DateTime to, [FromQuery] int availabilityViewInterval)
        {
            return Ok(await Mediator.Send(new GetRoomAvailabilityQuery
                {Email = email, From = from, To = to, AvailabilityViewInterval = availabilityViewInterval}));
        }

        /// <summary>
        /// Create a new room
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Nothing</returns>
        /// <response code="204">Success</response>
        /// <response code="400">Failure</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Create([FromBody]CreateRoomCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Updating details of the room
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns>Nothing</returns>
        /// <response code="204">Success</response>
        /// <response code="400">Failure</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Room not found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Update(string id, [FromBody]UpdateRoomCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Removes the room with the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Nothing</returns>
        /// <response code="204">Success</response>
        /// <response code="400">Failure</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Room not found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Delete(string id)
        {
            await Mediator.Send(new DeleteRoomCommand { Id = id });

            return NoContent();
        }
    }
}