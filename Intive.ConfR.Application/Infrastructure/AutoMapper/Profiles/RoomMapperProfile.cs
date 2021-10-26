using AutoMapper;
using Intive.ConfR.Application.Interfaces.Mapping;
using Intive.ConfR.Domain;
using Intive.ConfR.Domain.Entities;
using Intive.ConfR.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Intive.ConfR.Application.Infrastructure.AutoMapper.Profiles
{
    public class RoomMapperProfile : IHaveCustomMapping
    {
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<RoomDTO, Room>().
                ForMember(dest => dest.Email, opt => opt.MapFrom(r => (EMailAddress)r.Address));

        }
    }
}
