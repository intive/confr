using AutoMapper;
using Intive.ConfR.Application.Interfaces.Mapping;
using Intive.ConfR.Domain.Entities;

namespace Intive.ConfR.Application.Rooms.Queries.GetRoomsList
{
    public class RoomLookupModel : IHaveCustomMapping
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int SeatsNumber { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Room, RoomLookupModel>()
                .ForMember(cDTO => cDTO.Email, opt => opt.MapFrom(c => (string)c.Email));
        }

    }
}
