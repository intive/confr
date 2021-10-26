using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Intive.ConfR.Application.Photos.Commands.CreatePhoto;
using Intive.ConfR.Application.Photos.Commands.DeletePhoto;
using Intive.ConfR.Application.Photos.Commands.UpdatePhoto;
using Intive.ConfR.Application.Photos.Models;
using Intive.ConfR.Application.Photos.Queries.GetPhoto;
using Intive.ConfR.Application.Photos.Queries.GetPhotoList;
using Intive.ConfR.Application.Photos.Queries.GetThumbnail;
using Intive.ConfR.Application.Photos.Queries.GetThumbnailList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Intive.ConfR.API.Controllers
{
    [Authorize]
    [Route("api/Rooms/[controller]")]
    public class PhotosController : BaseController
    {
        /// <summary>
        /// Create a new photo
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Nothing</returns>
        /// <response code="201">Created photo in db</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="414">Uri or path too long</response>
        /// <response code="500">InternalServerError</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status414RequestUriTooLong)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromForm] CreatePhotoCommand command)
        {
            var url = await Mediator.Send(command);
            return Created(url, new {command.RoomEmail, Name = Uri.UnescapeDataString(new Uri(url).Segments.Last())});
        }

        /// <summary>
        /// Delete a photo
        /// </summary>
        /// <param name="email">Room email</param>
        /// <param name="command"></param>
        /// <returns>Nothing</returns>
        /// <response code="204">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Photo not found</response>
        /// <response code="414">Uri or path too long</response>
        /// <response code="500">InternalServerError</response>
        [HttpDelete("{email}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status414RequestUriTooLong)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(string email, [FromBody] DeletePhotoCommand command)
        {
            command.RoomEmail = email;
            await Mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Update an existing photo
        /// </summary>
        /// <param name="email">Room email</param>
        /// <param name="command"></param>
        /// <returns>Nothing</returns>
        /// <response code="204">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Photo not found</response>
        /// <response code="414">Uri or path too long</response>
        /// <response code="500">InternalServerError</response>
        [HttpPut("{email}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status414RequestUriTooLong)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(string email, [FromForm] UpdatePhotoCommand command)
        {
            command.RoomEmail = email;
            await Mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Get all photos for given email
        /// </summary>
        /// <param name="email">Room email</param>
        /// <param name="requireSas">Require Shared Access Signature</param>
        /// <returns>List of all photos</returns>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Container not found</response>
        /// <response code="500">InternalServerError</response>
        [HttpGet("{email}")]
        [ProducesResponseType(typeof(List<RoomPhotoDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll(string email, [FromQuery] bool requireSas)
        {
            return Ok(await Mediator.Send(new GetPhotoListQuery{Email = email, RequireSas = requireSas}));
        }

        /// <summary>
        /// Get a photo
        /// </summary>
        /// <param name="email">Room email</param>
        /// <param name="name">Photo name</param>
        /// <param name="requireSas">Require Shared Access Signature</param>
        /// <returns>Photo</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Photo not found</response>
        /// <response code="500">InternalServerError</response>
        [HttpGet("{email}/{name}")]
        [ProducesResponseType(typeof(RoomPhotoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(string email, string name, [FromQuery] bool requireSas)
        {
            return Ok(await Mediator.Send(new GetPhotoQuery{Email = email, Name = name, RequireSas = requireSas}));
        }

        /// <summary>
        /// Get all thumbnails for the given room
        /// </summary>
        /// <param name="email">Room email</param>
        /// <param name="requireSas">Require Shared Access Signature</param>
        /// <returns>List of thumbnails</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Thumbnail not found</response>
        /// <response code="500">InternalServerError</response>
        [HttpGet("{email}/thumbnails")]
        [ProducesResponseType(typeof(RoomPhotoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllThumbnails(string email, [FromQuery] bool requireSas)
        {
            return Ok(await Mediator.Send(new GetThumbnailListQuery { Email = email, RequireSas = requireSas }));
        }

        /// <summary>
        /// Get single thumbnail
        /// </summary>
        /// <param name="email">Room email</param>
        /// <param name="name">Original photo name</param>
        /// <param name="requireSas">Require Shared Access Signature</param>
        /// <returns>Thumbnail</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Thumbnail not found</response>
        /// <response code="500">InternalServerError</response>
        [HttpGet("{email}/{name}/thumbnail")]
        [ProducesResponseType(typeof(RoomPhotoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetThumbnail(string email, string name, [FromQuery] bool requireSas)
        {
            return Ok(await Mediator.Send(new GetThumbnailQuery { Email = email, Name = name, RequireSas = requireSas }));
        }
    }
}