using System;
using System.Text.RegularExpressions;
using Intive.ConfR.Application.Interfaces.AzureStorage;

namespace Intive.ConfR.Infrastructure.AzureStorage.Helpers
{
    public class NameGenerator : INameGenerator
    {
        public string GenerateContainerName(string email)
        {
            var name = Regex.Replace(email, "[^a-zA-Z0-9]", "-");

            return $"room-{name}";
        }

        public string GeneratePhotoName(string extension)
        {
            return $"room-photo-{DateTime.Now.ToFileTime()}.{extension}";
        }

        public string GenerateThumbnailName(string originalFileName)
        {
            return $"thumbnail-{originalFileName}";
        }
    }
}
