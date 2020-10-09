using Application.ApiKeys;
using Application.Common.Exceptions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using WebUI.Extensions;

namespace WebUI.Handler
{
    // based on: https://josef.codes/asp-net-core-protect-your-api-with-api-keys/
    public class ApiKeyAuthenticationHandler : AuthenticationHandler<ApiKeyAuthenticationOptions>
    {
        private const string API_KEY_HEADER_NAME = "X-Api-Key";
        private readonly ApiKeysService _apiKeyService;

        public ApiKeyAuthenticationHandler(
            IOptionsMonitor<ApiKeyAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            ApiKeysService apiKeyService) : base(options, logger, encoder, clock)
        {
            _apiKeyService = apiKeyService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.TryGetValue(API_KEY_HEADER_NAME, out var apiKeyHeaderValues))
            {
                return AuthenticateResult.NoResult();
            }

            var apiKeyFromHeader = apiKeyHeaderValues.FirstOrDefault();

            if (apiKeyHeaderValues.Count == 0 || string.IsNullOrWhiteSpace(apiKeyFromHeader))
            {
                return AuthenticateResult.NoResult();
            }

            var existingApiKey = await _apiKeyService.FindApiKeyByKey(apiKeyFromHeader);

            if (existingApiKey == null)
            {
                return AuthenticateResult.Fail("Invalid API key");
            } else
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, existingApiKey.Name)
                };

                var identity = new ClaimsIdentity(claims, Options.AuthenticationType);
                var identities = new List<ClaimsIdentity> { identity };
                var principal = new ClaimsPrincipal(identities);
                var ticket = new AuthenticationTicket(principal, Options.Scheme);
                return AuthenticateResult.Success(ticket);
            }
        }

        protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            throw new NotAuthorizedException();
        }
    }
}
