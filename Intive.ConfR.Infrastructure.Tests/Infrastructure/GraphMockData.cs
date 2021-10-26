using System;
using System.Collections.Generic;
using Intive.ConfR.Domain.Entities;
using Intive.ConfR.Domain.ValueObjects;

namespace Intive.ConfR.Infrastructure.Tests.Infrastructure
{
    public class GraphMockData
    {
        public static IList<Room> RoomsList()
        {
            return new List<Room>
            {
                new Room
                {
                    Name = "Black",
                    Email = EMailAddress.For("black@patronage.onmicrosoft.com"),
                    Location = "1.17",
                    PhoneNumber = "",
                    City = "Szczecin",
                    StreetAddress = "Hołdu pruskiego 9"
                },
                new Room
                {
                    Name = "Pink",
                    Email = EMailAddress.For("pink@patronage.onmicrosoft.com"),
                    Location = "4.56",
                    PhoneNumber = "",
                    City = "Szczecin",
                    StreetAddress = "Hołdu pruskiego 9"
                },
                new Room
                {
                    Name = "White",
                    Email = EMailAddress.For("white@patronage.onmicrosoft.com"),
                    Location = "4.50",
                    PhoneNumber = "",
                    City = "Szczecin",
                    StreetAddress = "Hołdu pruskiego 9"
                }
            };
        }

        public static GraphUser GetUserData()
        {
            return new GraphUser
            {
                Id = Guid.Parse("9fe919f4-427b-4c3a-957c-fdec51178e34"),
                DisplayName = "Rick Sanchez",
                GivenName = "Rick",
                Surname = "Sanchez",
                Mail = "rick.sanchez@patronage.onmicrosoft.com",
                UserType = "Member",
                UsageLocation = "PL"
            };
        }
    }
}
