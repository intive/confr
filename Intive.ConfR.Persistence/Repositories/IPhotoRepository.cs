using Intive.ConfR.Domain;
using Intive.ConfR.Domain.Entities;
using Intive.ConfR.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Intive.ConfR.Persistence
{
    public interface IPhotoRepository
    {
        Task<IList<PhotoUrl>> GetPhotos(EMailAddress roomEmail);

        Task AddPhoto(PhotoUrl photoUrl);
    }
}
