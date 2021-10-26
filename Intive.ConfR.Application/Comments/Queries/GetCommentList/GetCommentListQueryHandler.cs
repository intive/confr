using AutoMapper;
using Intive.ConfR.Application.Comments.Shared;
using Intive.ConfR.Application.Interfaces;
using Intive.ConfR.Domain;
using Intive.ConfR.Domain.Entities;
using Intive.ConfR.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Intive.ConfR.Application.Comments.Queries.GetCommentList
{
    public class GetCommentListQueryHandler : IRequestHandler<GetCommentListQuery, List<CommentDTO>>
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public GetCommentListQueryHandler(ICommentService commentService, IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }

        public async Task<List<CommentDTO>> Handle(GetCommentListQuery request, CancellationToken cancellationToken)
        {
            var roomEmail = Utility.ToEMailAddress(request.RoomEmail, _mapper);

            var comments = await _commentService.GetComments(roomEmail);

            return _mapper.Map<List<CommentDTO>>(comments);
        }
    }
}
