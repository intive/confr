using Intive.ConfR.Domain;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Intive.ConfR.Application.Interfaces;
using Intive.ConfR.Domain.Entities;
using Intive.ConfR.Application.Comments.Shared;

namespace Intive.ConfR.Application.Comments.Queries.GetComment
{
    public class GetCommentQueryHandler : IRequestHandler<GetCommentQuery, CommentDTO>
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public GetCommentQueryHandler(ICommentService commentService, IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }

        public async Task<CommentDTO> Handle(GetCommentQuery request, CancellationToken cancellationToken)
        {
            Guid commentId = Utility.ToGuid(request.CommentId, _mapper);

            var comment = await _commentService.GetComment(commentId);

            var result = _mapper.Map<Comment, CommentDTO>(comment);

            return result;
        }
    }
}
