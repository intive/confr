using AutoMapper;
using Intive.ConfR.Application.Comments.Shared;
using Intive.ConfR.Application.Exceptions;
using Intive.ConfR.Application.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Intive.ConfR.Application.Comments.Commands.CreateComment
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand>
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public CreateCommentCommandHandler(ICommentService commentService, IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var roomEmail = Utility.ToEMailAddress(request.RoomEmail, _mapper);

            await _commentService.CreateComment(roomEmail, request.Body);

            return Unit.Value;
        }
    }
}
