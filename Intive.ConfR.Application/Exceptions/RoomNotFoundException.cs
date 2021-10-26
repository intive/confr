using Intive.ConfR.Domain.ValueObjects;

namespace Intive.ConfR.Application.Exceptions
{
    public class RoomNotFoundException : NotFoundException
    {
        public RoomNotFoundException(EMailAddress roomEmail) :
            base($"Room with the given email \"{roomEmail}\" not found!")
        {

        }
    }
}
