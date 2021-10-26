using Intive.ConfR.Application.Interfaces;
using Intive.ConfR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Intive.ConfR.Application.Exceptions;
using Intive.ConfR.Domain.ValueObjects;

namespace Intive.ConfR.Infrastructure
{
    public class MockRoomService : IRoomService
    {
        public async Task<IList<Room>> GetRooms()
        {
            List<Room> rooms = new List<Room>()
            {
                new Room()
                {
                    Name = "Conf Room Damian",
                    Email = (EMailAddress) "confroomdamian@patronage.onmicrosoft.com",
                    Id = "1",
                    Location = "Szczecin",
                    SeatsNumber = 20,
                    PhoneNumber = "123-456-789"
                },
                new Room()
                {
                    Name = "Jerry and Beth's Room",
                    Email = (EMailAddress) "jerryandbethsroom@patronage.onmicrosoft.com",
                    Id = "2",
                    Location = "Szczecin",
                    SeatsNumber = 10,
                    PhoneNumber = "123-456-789"
                },
                new Room()
                {
                    Name = "Mateusz Room",
                    Email = (EMailAddress) "mateuszroom@patronage.onmicrosoft.com",
                    Id = "3",
                    Location = "Szczecin",
                    SeatsNumber = 20,
                    PhoneNumber = "123-456-789"
                },
                new Room()
                {
                    Name = "Rick's Room",
                    Email = (EMailAddress) "ricksroom@patronage.onmicrosoft.com",
                    Id = "4",
                    Location = "Szczecin",
                    SeatsNumber = 40,
                    PhoneNumber = "123-456-789"
                },
                new Room()
                {
                    Name = "super damian room",
                    Email = (EMailAddress) "superdamianroom@patronage.onmicrosoft.com",
                    Id = "5",
                    Location = "Szczecin",
                    SeatsNumber = 30,
                    PhoneNumber = "123-456-789"
                },
                new Room()
                {
                    Name = "test",
                    Email = (EMailAddress) "test@patronage.onmicrosoft.com",
                    Id = "6",
                    Location = "Szczecin",
                    SeatsNumber = 15,
                    PhoneNumber = "123-456-789"
                },
                new Room()
                {
                    Name = "The Garage",
                    Email = (EMailAddress) "thegarage@patronage.onmicrosoft.com",
                    Id = "7",
                    Location = "Szczecin",
                    SeatsNumber = 20,
                    PhoneNumber = "123-456-789"
                }
            };

            return rooms;
        }

        public Task<List<Room>> GetRoomsBasicList()
        {
            throw new NotImplementedException();
        }

        public IDictionary<EMailAddress, IList<Reservation>> GetReservations(List<Schedule> schedules)
        {
            var reservations = new Dictionary<EMailAddress, IList<Reservation>>
            {
                { (EMailAddress) "confroomdamian@patronage.onmicrosoft.com", new List<Reservation>()
                    {
                        new Reservation()
                        {
                             Subject = "1 reservation",
                             StartTime = new DateTime(2019,3,13,10,20,0),
                             EndTime = new DateTime(2019,3,13,10,50,0)
                        },
                        new Reservation()
                        {
                             Subject = "2 reservation",
                             StartTime = new DateTime(2019,3,13,11,20,0),
                             EndTime = new DateTime(2019,3,13,12,50,0)
                        },
                        new Reservation()
                        {
                             Subject = "3 reservation",
                             StartTime = new DateTime(2019,3,14,10,20,0),
                             EndTime = new DateTime(2019,3,14,13,50,0)
                        },
                    }
                },
                { (EMailAddress) "mateuszroom@patronage.onmicrosoft.com", new List<Reservation>()
                    {
                        new Reservation()
                        {
                             Subject = "4 reservation",
                             StartTime = new DateTime(2019,3,13,12,20,0),
                             EndTime = new DateTime(2019,3,13,14,50,0)
                        },
                        new Reservation()
                        {
                             Subject = "5 reservation",
                             StartTime = new DateTime(2019,3,13,11,20,0),
                             EndTime = new DateTime(2019,3,13,12,50,0)
                        },
                        new Reservation()
                        {
                             Subject = "6 reservation",
                             StartTime = new DateTime(2019,3,15,11,20,0),
                             EndTime = new DateTime(2019,3,15,13,50,0)
                        },
                    }
                },
                { (EMailAddress) "jerryandbethsroom@patronage.onmicrosoft.com", new List<Reservation>() {}
                },
                { (EMailAddress) "ricksroom@patronage.onmicrosoft.com", new List<Reservation>() {}
                },
                { (EMailAddress) "superdamianroom@patronage.onmicrosoft.com", new List<Reservation>() {}
                },
                { (EMailAddress) "test@patronage.onmicrosoft.com", new List<Reservation>() {}
                },
                { (EMailAddress) "thegarage@patronage.onmicrosoft.com", new List<Reservation>() {}
                }
            };

            return reservations;
        }

        public async Task<Room> GetRoomByEmail(string email)
        {
            var rooms = await GetRooms();

            var room = rooms.SingleOrDefault(r => r.Email == email);

            if (room == null)
            {
                throw new NotFoundException(nameof(Room), email);
            }

            return room;
        }

        public Task<List<Schedule>> GetSchedulesList(ScheduleRequest body)
        {
            throw new NotImplementedException();
        }

        public bool CheckRoomsAvailability(List<Schedule> schedules)
        {
            throw new NotImplementedException();
        }

        public List<Schedule> PickRandomSchedule(List<Schedule> schedules)
        {
            throw new NotImplementedException();
        }

        public Task<List<GraphReservation>> GetReservationsList(string email, DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }
    }
}
