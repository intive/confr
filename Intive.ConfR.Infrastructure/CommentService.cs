using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Intive.ConfR.Application.Interfaces;
using Intive.ConfR.Domain.Entities;
using Intive.ConfR.Domain.ValueObjects;
using Intive.ConfR.Persistence.Repositories;
using Intive.ConfR.Application.Exceptions;

namespace Intive.ConfR.Infrastructure
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _repository;
        private readonly IRoomsDirectory _rooms;
        private readonly IUserService _user;

        public CommentService(ICommentRepository repository, IRoomsDirectory rooms, IUserService user)
        {
            _repository = repository;
            _rooms = rooms;
            _user = user;
        }

        public async Task CreateComment(EMailAddress roomEmail, string body)
        {
            if (!await _rooms.RoomExists(roomEmail))
            {
                throw new RoomNotFoundException(roomEmail);
            }

            var userName = await _user.GetPersonalData();

            await _repository.AddComment(new Comment
            {
                CommentId = new Guid(),
                RoomEmail = roomEmail,
                Body = body,
                CreatedDateTime = DateTimeOffset.Now,
                LastModifiedDateTime = DateTimeOffset.Now,
                UserDisplayName = userName.DisplayName,
                UserId = userName.Id
            });
        }

        public async Task DeleteComment(Guid commentId)
        {
            if (!await IsAuthorOfComment(commentId))
            {
                throw new ForbiddenException("You are not allowed to delete this comment since you are not an author.");
            }

            await _repository.RemoveComment(commentId);
        }

        public async Task<Comment> GetComment(Guid commentId)
        {
            return await _repository.GetCommentById(commentId);
        }

        public async Task<IList<Comment>> GetComments(EMailAddress roomEmail)
        {
            return await _rooms.RoomExists(roomEmail)
                ? await _repository.GetCommentsList(roomEmail)
                : throw new RoomNotFoundException(roomEmail);
        }

        public async Task UpdateComment(Guid commentId, string body)
        {
            if (!await IsAuthorOfComment(commentId))
            {
                throw new ForbiddenException("You are not allowed to modify this comment since you are not an author.");
            }

            await _repository.ChangeComment(commentId, body);
        }

        public async Task<bool> IsAuthorOfComment(Guid commentId)
        {
            var user = await _user.GetPersonalData();
            var userId = user.Id;

            var comment = await GetComment(commentId);
            var authorId = comment.UserId;

            return userId == authorId;
        }
    }
}
