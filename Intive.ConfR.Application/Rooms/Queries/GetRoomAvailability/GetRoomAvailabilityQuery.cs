using System;
using System.Collections.Generic;
using AutoMapper;
using Intive.ConfR.Application.Interfaces.Mapping;
using Intive.ConfR.Domain.Entities;
using MediatR;

namespace Intive.ConfR.Application.Rooms.Queries.GetRoomAvailability
{
    public class GetRoomAvailabilityQuery : IRequest<RoomAvailabilityViewModel>, IHaveCustomMapping
    {
        public string Email { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int AvailabilityViewInterval { get; set; } = 30;

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<GetRoomAvailabilityQuery, ScheduleRequest>()
                .ForMember(sr => sr.Schedules,
                    opt => opt.MapFrom(grrq => new List<string> { grrq.Email }))
                .ForMember(sr => sr.StartTime,
                    opt => opt.MapFrom(grrq => new DateTimeTimeZone
                    { DateTime = grrq.From }))
                .ForMember(sr => sr.EndTime,
                    opt => opt.MapFrom(grrq => new DateTimeTimeZone
                    { DateTime = grrq.To }))
                    .ForMember(sr => sr.AvailabilityViewInterval, opt => opt.MapFrom(grrq => grrq.AvailabilityViewInterval));
        }
    }
}
