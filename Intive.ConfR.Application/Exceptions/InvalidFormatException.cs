using System;
using System.Collections.Generic;
using System.Text;

namespace Intive.ConfR.Application.Exceptions
{
    public class InvalidFormatException : Exception
    {
        public InvalidFormatException(string message) : base(message)
        {
        }
    }
}
