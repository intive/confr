using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intive.ConfR.Domain;
using Intive.ConfR.Domain.Entities;
using Intive.ConfR.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Intive.ConfR.Persistence.Repositories
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly ConfRContext _context;
        public PhotoRepository(ConfRContext context)
        {
            _context = context;
        }

        public async Task AddPhoto(PhotoUrl photoUrl)
        {
            await _context.AddAsync(photoUrl);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<PhotoUrl>> GetPhotos(EMailAddress roomEmail)
        {
            return await _context.Photos.Where(x => x.RoomEmail == roomEmail).ToListAsync();
        }
    }
}
