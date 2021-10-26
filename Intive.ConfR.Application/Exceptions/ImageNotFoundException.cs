using System;

namespace Intive.ConfR.Application.Exceptions
{
    public class ImageNotFoundException : Exception
    {
        public ImageNotFoundException(string name, string container) :
            base($"Image \"{name}\" not found in container \"{container}\"!")
        {
            
        }
    }
}
