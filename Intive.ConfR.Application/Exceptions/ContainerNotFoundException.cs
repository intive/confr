using System;

namespace Intive.ConfR.Application.Exceptions
{
    public class ContainerNotFoundException : Exception
    {
        public ContainerNotFoundException(string name) : 
            base($"Blob container \"{name}\" not found!")
        { }
    }
}
