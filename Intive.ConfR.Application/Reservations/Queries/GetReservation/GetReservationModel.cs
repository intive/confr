using AutoMapper;
using Intive.ConfR.Application.Interfaces.Mapping;
using Intive.ConfR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Intive.ConfR.Application.Reservations.Queries.GetReservation
{
    public class GetReservationModel : IHaveCustomMapping
    {
        public string Id { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public List<string> Locations { get; set; }
        public List<Attendee> Attendees { get; set; }
        public Organizer Organizer { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<GraphReservation, GetReservationModel>()
                .ForMember(des => des.Body,      opt => opt.MapFrom(src => src.Body.Content))
                .ForMember(des => des.Start,     opt => opt.MapFrom(src => src.Start.DateTime))
                .ForMember(des => des.End,       opt => opt.MapFrom(src => src.End.DateTime))
                .ForMember(des => des.Locations, opt => opt.MapFrom(src => src.Locations.Select(x => x.DisplayName)));
        }
    }
}
