using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Intive.ConfR.Domain.Entities;
using Intive.ConfR.Domain.ValueObjects;

namespace Intive.ConfR.Application.Interfaces
{
    public interface ICommentService
    {
        Task CreateComment(EMailAddress roomEmail, string body);
        Task UpdateComment(Guid commentId, string body);
        Task DeleteComment(Guid commentId);
        Task<Comment> GetComment(Guid commentId);
        Task<IList<Comment>> GetComments(EMailAddress roomEmail);
        Task<bool> IsAuthorOfComment(Guid commentId);
    }
}
