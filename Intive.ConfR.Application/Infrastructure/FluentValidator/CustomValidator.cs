using System;

namespace Intive.ConfR.Application.Infrastructure.FluentValidator
{
    public static class CustomValidator
    {
        public static bool ValidateGuid(string guid)
        {
            return Guid.TryParse(guid, out _);
        }
    }
}
