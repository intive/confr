using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Intive.ConfR.Application.Comments.Commands.CreateComment;
using Intive.ConfR.Application.Comments.Commands.DeleteComment;
using Intive.ConfR.Application.Comments.Queries.GetCommentList;
using Intive.ConfR.Domain;
using System.Collections.Generic;
using Intive.ConfR.Application.Comments.Queries.GetComment;
using Intive.ConfR.Application.Comments.Commands.UpdateComment;
using Microsoft.AspNetCore.Authorization;

namespace Intive.ConfR.API.Controllers
{
    [Authorize]
    [Route("api/Rooms/[controller]")]
    public class CommentsController : BaseController
    {
        /// <summary>
        /// Create a new comment for the room with the given email
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Nothing</returns>
        /// <response code="204">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Room not found</response>
        /// <response code="422">Invalid input format</response>
        /// <response code="500">InternalServerError</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateCommentCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Update a comment
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Nothing</returns>
        /// <response code="204">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Comment not found</response>
        /// <response code="422">Invalid input format</response>
        /// <response code="500">InternalServerError</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] UpdateCommentCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Delete a comment
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns>Nothing</returns>
        /// <response code="204">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Comment not found</response>
        /// <response code="422">Invalid input format</response>
        /// <response code="500">InternalServerError</response>
        [HttpDelete("{commentId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(string commentId)
        {
            await Mediator.Send(new DeleteCommentCommand { CommentId = commentId });

            return NoContent();
        }

        /// <summary>
        /// Get all comments for the room with the given email
        /// </summary>
        /// <param name="email"></param>
        /// <returns>List of all comments</returns>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Room not found</response>
        /// <response code="422">Invalid input format</response>
        /// <response code="500">InternalServerError</response>
        [HttpGet("all/{email}")]
        [ProducesResponseType(typeof(List<CommentDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll(string email)
        {
            return Ok(await Mediator.Send(new GetCommentListQuery { RoomEmail = email }));
        }

        /// <summary>
        /// Get a comment
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns>Comment</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Comment not found</response>
        /// <response code="422">Invalid input format</response>
        /// <response code="500">InternalServerError</response>
        [HttpGet("{commentId}")]
        [ProducesResponseType(typeof(CommentDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(string commentId)
        {
            return Ok(await Mediator.Send(new GetCommentQuery { CommentId = commentId }));
        }

    }
}