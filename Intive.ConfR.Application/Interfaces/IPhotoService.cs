using System.Collections.Generic;
using System.Threading.Tasks;
using Intive.ConfR.Domain.ValueObjects;

namespace Intive.ConfR.Application.Interfaces
{
    public interface IPhotoService
    {
        Task SavePhoto(EMailAddress roomEmail, string photoUrl);
        Task<IList<string>> GetPhotoUrls(EMailAddress roomEmail);
    }
}
