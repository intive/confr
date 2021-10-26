using System;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Intive.ConfR.API.Extensions
{
    public static class FakeAuthenticationExtensions
    {
        public static AuthenticationBuilder AddFakeAuth(this AuthenticationBuilder builder, Action<FakeAuthenticationOptions> configureOptions)
        {
            return builder.AddScheme<FakeAuthenticationOptions, FakeAuthenticationHandler>("FakeScheme", "FakeAuth", configureOptions);
        }
    }

    public class FakeAuthenticationHandler : AuthenticationHandler<FakeAuthenticationOptions>
    {
        public FakeAuthenticationHandler(IOptionsMonitor<FakeAuthenticationOptions> options, ILoggerFactory logger,
            UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var ticket = new AuthenticationTicket(new ClaimsPrincipal(Options.Identity), "FakeScheme");

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }

    public class FakeAuthenticationOptions : AuthenticationSchemeOptions
    {
        public virtual ClaimsIdentity Identity { get; } = new ClaimsIdentity(new Claim[]
        {
            new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", Guid.NewGuid().ToString()),
        }, "Fake :)");
    }
}
