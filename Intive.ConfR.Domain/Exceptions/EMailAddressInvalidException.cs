using System;

namespace Intive.ConfR.Domain.Exceptions
{
    public class EMailAddressInvalidException : Exception
    {
        public EMailAddressInvalidException(string addressString, Exception exception)
           : base($"E-mail address \"{addressString}\" is invalid.", exception)
        { }
    }
}
