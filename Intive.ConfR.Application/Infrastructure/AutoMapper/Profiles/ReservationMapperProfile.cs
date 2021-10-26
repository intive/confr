using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Intive.ConfR.Application.Interfaces.Mapping;
using Intive.ConfR.Application.Reservations.Commands.CreateRandomReservation;
using Intive.ConfR.Application.Reservations.Commands.CreateReservation;
using Intive.ConfR.Domain.Entities;

namespace Intive.ConfR.Application.Infrastructure.AutoMapper.Profiles
{
    public class ReservationMapperProfile : IHaveCustomMapping
    {
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<CreateReservationCommand, ReservationRequest>()
               .ForMember(rr => rr.Body,
                   opt => opt.MapFrom(crc => new ItemBody { Content = crc.Content }))
               .ForMember(rr => rr.Start,
                   opt => opt.MapFrom(crc => new DateTimeTimeZone { DateTime = crc.From }))
               .ForMember(rr => rr.End,
                   opt => opt.MapFrom(crc => new DateTimeTimeZone { DateTime = crc.To }))
               .ForMember(rr => rr.Attendees, opt => opt.Ignore());

            configuration.CreateMap<List<Room>, ReservationRequest>()
                .ForMember(rr => rr.Locations,
                    opt => opt.MapFrom(rL => rL.Select(r => new Location
                    { DisplayName = r.Name, LocationUri = r.Email })))
                .ForMember(rr => rr.Attendees,
                    opt => opt.MapFrom(rL => rL.Select(r => new Attendee
                    { Type = "resource", EmailAddress = new GraphEmailAddress { Name = r.Name, Address = r.Email } })));

            configuration.CreateMap<CreateReservationCommand, ScheduleRequest>()
                .ForMember(sr => sr.Schedules,
                    opt => opt.MapFrom(crc => crc.Rooms))
                .ForMember(sr => sr.StartTime,
                    opt => opt.MapFrom(crc => new DateTimeTimeZone { DateTime = crc.From }))
                .ForMember(sr => sr.EndTime,
                    opt => opt.MapFrom(crc => new DateTimeTimeZone { DateTime = crc.To }));


            configuration.CreateMap<CreateRandomReservationCommand, ReservationRequest>()
                .ForMember(rr => rr.Body,
                    opt => opt.MapFrom(crrc => new ItemBody { Content = crrc.Content }))
                .ForMember(rr => rr.Start,
                    opt => opt.MapFrom(crrc => new DateTimeTimeZone { DateTime = crrc.From }))
                .ForMember(rr => rr.End,
                    opt => opt.MapFrom(crrc => new DateTimeTimeZone { DateTime = crrc.To }))
                .ForMember(rr => rr.Attendees, opt => opt.Ignore());

            configuration.CreateMap<CreateRandomReservationCommand, ScheduleRequest>()
                .ForMember(sr => sr.StartTime,
                    opt => opt.MapFrom(crrc => new DateTimeTimeZone { DateTime = crrc.From }))
                .ForMember(sr => sr.EndTime,
                    opt => opt.MapFrom(crrc => new DateTimeTimeZone { DateTime = crrc.To }));

            configuration.CreateMap<GraphReservation, Reservation>()
                .ForMember(r => r.Id, opt => opt.MapFrom(gr => gr.Id))
                .ForMember(r => r.Subject, opt => opt.MapFrom(gr => gr.Subject))
                .ForMember(r => r.StartTime, opt => opt.MapFrom(gr => gr.Start.DateTime))
                .ForMember(r => r.EndTime, opt => opt.MapFrom(gr => gr.End.DateTime))
                .ForMember(r => r.Owner, opt => opt.MapFrom(gr => gr.Organizer.EmailAddress.Address));
        }
    }
}
