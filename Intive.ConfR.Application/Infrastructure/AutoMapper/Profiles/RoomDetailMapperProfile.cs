using System.Linq;
using AutoMapper;
using Intive.ConfR.Application.Interfaces.Mapping;
using Intive.ConfR.Domain.Entities;
using Intive.ConfR.Domain.ValueObjects;

namespace Intive.ConfR.Application.Infrastructure.AutoMapper.Profiles
{
    public class RoomDetailMapperProfile : IHaveCustomMapping
    {
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<GraphDetailedRoom, Room>().ConstructUsing(r => new Room
            {
                Name = r.DisplayName,
                Email = EMailAddress.For(r.Mail),
                Location = r.OfficeLocation,
                SeatsNumber = 10,                   //Graph doesn't have seats number.
                PhoneNumber = r.BusinessPhones.FirstOrDefault()
            });
        }
    }
}
