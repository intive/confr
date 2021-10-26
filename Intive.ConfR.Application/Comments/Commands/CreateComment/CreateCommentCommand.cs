using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Intive.ConfR.Application.Comments.Commands.CreateComment
{
    public class CreateCommentCommand : IRequest
    {
        [Required]
        public string RoomEmail { get; set; }
        [Required]
        public string Body { get; set; }
    }
}
