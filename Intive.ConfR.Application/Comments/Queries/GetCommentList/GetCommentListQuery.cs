using Intive.ConfR.Domain;
using MediatR;
using System.Collections.Generic;

namespace Intive.ConfR.Application.Comments.Queries.GetCommentList
{
    public class GetCommentListQuery : IRequest<List<CommentDTO>>
    {
        public string RoomEmail { get; set; }
    }
}
