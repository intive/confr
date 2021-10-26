using System.Collections.Generic;
using System.Threading.Tasks;
using Intive.ConfR.Application.Interfaces;
using Intive.ConfR.Domain.Entities;
using Intive.ConfR.Infrastructure.ApiClient;
using Microsoft.AspNetCore.Http;

namespace Intive.ConfR.Infrastructure
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly MicrosoftGraphApiClient _client;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _client = new MicrosoftGraphApiClient();
        }

        /// <summary>
        /// Returns personal information from Microsoft Graph
        /// </summary>
        /// <returns>object of <see cref="GraphUser"/></returns>
        public async Task<GraphUser> GetPersonalData()
        {
            var query = new Dictionary<string, string>
            {
                { "$select", "id,displayName,givenName,surname,mail,userType,usageLocation"},
            };

            var request = new GraphApiGetRequest
            {
                GraphVersion = "beta/",
                Endpoint = "me",
                QueryParameters = query
            };

            var httpContext = _httpContextAccessor.HttpContext;

            return await _client.Get<GraphUser>(request, httpContext);
        }
    }
}
