using Intive.ConfR.Domain.Entities;
using Intive.ConfR.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Intive.ConfR.Persistence.Repositories.Exceptions;

namespace Intive.ConfR.Persistence.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ConfRContext _context;

        public CommentRepository(ConfRContext context)
        {
            _context = context;
        }

        public async Task AddComment(Comment comment)
        {
            await _context.AddAsync(comment);

            await _context.SaveChangesAsync();
        }

        public async Task RemoveComment(Guid commentId)
        {
            var commentRemoved = await GetCommentById(commentId);

            _context.Comments.RemoveRange(commentRemoved);

            await _context.SaveChangesAsync();
        }

        public async Task ChangeComment(Guid commentId, string body)
        {
            var comment = await GetCommentById(commentId);

            comment.Body = body;

            comment.LastModifiedDateTime = DateTimeOffset.Now;

            await _context.SaveChangesAsync();
        }

        public async Task<Comment> GetCommentById(Guid commentId)
        {
            var result = await _context.Comments.FindAsync(commentId);

            if (result == null)
                throw new CommentNotFoundException(commentId);

            return result;
        }

        public async Task<IList<Comment>> GetCommentsList(EMailAddress roomEmail)
        {
            return await _context.Comments.Where(x => x.RoomEmail == roomEmail).ToListAsync();
        }
    }
}
