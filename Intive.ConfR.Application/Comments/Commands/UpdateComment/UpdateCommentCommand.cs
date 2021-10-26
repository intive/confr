using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Intive.ConfR.Application.Comments.Commands.UpdateComment
{
    public class UpdateCommentCommand : IRequest
    {
        [Required]
        public string CommentId { get; set; }
        [Required]
        public string Body { get; set; }
    }
}
