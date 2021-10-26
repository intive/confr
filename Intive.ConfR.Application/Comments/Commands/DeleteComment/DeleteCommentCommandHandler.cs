using AutoMapper;
using Intive.ConfR.Application.Comments.Shared;
using Intive.ConfR.Application.Exceptions;
using Intive.ConfR.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Intive.ConfR.Application.Comments.Commands.DeleteComment
{
    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand>
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public DeleteCommentCommandHandler(ICommentService commentService, IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            Guid commentId = Utility.ToGuid(request.CommentId, _mapper);

            await _commentService.DeleteComment(commentId);

            return Unit.Value;
        }
    }
}
