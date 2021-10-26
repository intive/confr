using Intive.ConfR.Domain.Entities;
using Intive.ConfR.Domain.ValueObjects;
using Intive.ConfR.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Intive.ConfR.Application.Interfaces;

namespace Intive.ConfR.Infrastructure
{
    public class PhotoService : IPhotoService
    {
        private readonly IPhotoRepository _repository;

        public PhotoService(IPhotoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IList<string>> GetPhotoUrls(EMailAddress roomEmail)
        {
            var rooms = await _repository.GetPhotos(roomEmail);

            return rooms.Select(room => room.Url).ToList();
        }

        public async Task SavePhoto(EMailAddress roomEmail, string photoUrl)
        {
            await _repository.AddPhoto(new PhotoUrl
            {
                Id = new Guid(),
                RoomEmail = roomEmail,
                Url = photoUrl
            });
        }
    }
}
