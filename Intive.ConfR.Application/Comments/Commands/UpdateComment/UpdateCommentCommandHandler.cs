using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Intive.ConfR.Application.Comments.Shared;
using Intive.ConfR.Application.Exceptions;
using Intive.ConfR.Application.Interfaces;
using MediatR;

namespace Intive.ConfR.Application.Comments.Commands.UpdateComment
{
    public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand>
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public UpdateCommentCommandHandler(ICommentService commentService, IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            Guid commentId = Utility.ToGuid(request.CommentId, _mapper);

            await _commentService.UpdateComment(commentId, request.Body);

            return Unit.Value;
        }
    }
}
