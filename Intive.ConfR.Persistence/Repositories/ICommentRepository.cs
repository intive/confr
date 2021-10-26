using Intive.ConfR.Domain.Entities;
using Intive.ConfR.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Intive.ConfR.Persistence.Repositories
{
    public interface ICommentRepository
    {
        Task AddComment(Comment comment);

        Task RemoveComment(Guid commentId);

        Task ChangeComment(Guid commentId, string body);

        Task<Comment> GetCommentById(Guid commentId);

        Task<IList<Comment>> GetCommentsList(EMailAddress roomEmail);
    }
}
