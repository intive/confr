using System.Linq;
using Intive.ConfR.Domain.Entities;
using Intive.ConfR.Domain.ValueObjects;

namespace Intive.ConfR.Persistence
{
    public static class ConfrInitializer
    {
        public static void Initialize(ConfRContext context)
        {
            context.Database.EnsureCreated();

            //TODO: Change Photos -> Rooms
            if (context.Photos.Any())
            {
                return;
            }

            SeedRooms(context);
        }

        //TODO: SeedRooms
        private static void SeedRooms(ConfRContext context)
        {
            
        }

        // Example
        private static void SeedPhotos(ConfRContext context)
        {
            var photos = new[]
            {
                new PhotoUrl{RoomEmail = EMailAddress.For("black@patronage.onmicrosoft.com"), Url = "img1"}, 
                new PhotoUrl{RoomEmail = EMailAddress.For("black@patronage.onmicrosoft.com"), Url = "img2"}, 
            };

            context.Photos.AddRange(photos);
            context.SaveChanges();
        }
    }
}
