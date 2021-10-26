using System;
using System.Linq.Expressions;
using Intive.ConfR.Domain.Entities;

//lecture: https://benjii.me/2018/01/expression-projection-magic-entity-framework-core/

namespace Intive.ConfR.Application.Rooms.Queries.GetRoomDetail
{
    public class GetRoomDetailModel
    {
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string Department { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public int SeatsNumber { get; set; }
        public string PhoneNumber { get; set; }

        public static Expression<Func<Room, GetRoomDetailModel>> Projection
        {
            get
            {
                return Room => new GetRoomDetailModel
                {
                    Email = Room.Email,
                    City = Room.City,
                    SeatsNumber = Room.SeatsNumber,
                    Name = Room.Name,
                    PhoneNumber = Room.PhoneNumber,
                    CompanyName = Room.CompanyName,
                    Department = Room.Department,
                    Location = Room.Location,
                    StreetAddress = Room.StreetAddress
                };
            }
        }

        public static GetRoomDetailModel Create(Room Room)
        {
            return Projection.Compile().Invoke(Room);
        }
    }
}
