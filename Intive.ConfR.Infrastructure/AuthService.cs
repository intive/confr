using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Intive.ConfR.Application.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace Intive.ConfR.Infrastructure
{
    public class AuthService : IAuthService 
    {
        private readonly AuthorizationData _options;
        private string _token;

        public AuthService(IOptions<AuthorizationData> options)
        {
            _options = options.Value;
        }
        
        public async Task<string> GetAccessToken()
        {
            if (_token != null)
            {
                return _token;
            }

            var client = new HttpClient();

            var url = _options.AuthUrl;

            var body = $"scope={_options.Scope}" +
                       $"&client_id={_options.ClientId}" +
                       $"&grant_type={_options.GrantType}" +
                       $"&username={_options.Username}" +
                       $"&password={_options.Password}" +
                       $"&client_secret={_options.ClientSecret}";

            var stringContent = new StringContent(body, Encoding.UTF8, "application/x-www-form-urlencoded");

            var result = await client.PostAsync(url, stringContent);
            var content = await result.Content.ReadAsStringAsync();

            var json = JObject.Parse(content);

            _token = json["access_token"].ToString();

            return _token;
        }
    }

    public class AuthorizationData
    {
        public string AuthUrl { get; set; }
        public string Scope { get; set; }
        public string ClientId { get; set; }
        public string GrantType { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ClientSecret { get; set; }
    }
}
