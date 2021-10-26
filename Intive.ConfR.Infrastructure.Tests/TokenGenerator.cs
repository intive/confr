using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Globalization;
using System.Threading.Tasks;

namespace Intive.ConfR.Infrastructure.Tests
{
    public static class TokenGenerator
    {
        private static readonly string clientId = "50dd4cf2-61c2-47a8-99ac-079b2cf82ca3";
        private static readonly string addInstance = "https://login.microsoftonline.com/{0}";
        private static readonly string tenant = "67cf6398-28ec-44cd-b3c0-881f597f02f3";
        private static readonly string resource = "https://graph.microsoft.com";
        private static readonly string appKey = "wdKUZB045_]jhzorGTD06;~";
        private static readonly string authority = string.Format(CultureInfo.InvariantCulture, addInstance, tenant);

        private static AuthenticationContext context = null;
        private static ClientCredential credential = null;

        public static async Task<string> GetToken()
        {
            context = new AuthenticationContext(authority);
            credential = new ClientCredential(clientId, appKey);

            AuthenticationResult result = null;
            string accessToken = null;
            result = await context.AcquireTokenAsync(resource, credential);
            accessToken = result.AccessToken;
            return accessToken;
        }
    }
}
