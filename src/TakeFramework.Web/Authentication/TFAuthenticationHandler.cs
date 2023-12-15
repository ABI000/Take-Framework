using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System.Security.Claims;
using System.Text.Encodings.Web;
using TakeFramework.JWT;

namespace TakeFramework.Web.Authentication
{
    /// <summary>
    /// 验证用户身份是否符合当前SchemeName的认证逻辑
    /// </summary>
    public class TFAuthenticationHandler(IJwt jwt,
        IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
    {
        private readonly IJwt jwt = jwt;
        public const string SchemeName = "TF";

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            Request.Headers.TryGetValue("Authorization", out StringValues values);
            string? valStr = values.FirstOrDefault();
            if (string.IsNullOrWhiteSpace(valStr))
            {
                return AuthenticateResult.Fail("Unauthorised");
            }
            (bool check, ClaimsPrincipal claimsPrincipal) = await jwt.ValidateTokenAsync(valStr);
            if (!check)
            {
                return AuthenticateResult.Fail("Unauthorised");
            }
            else
            {
                return AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, SchemeName));
            }
        }
        protected override Task HandleForbiddenAsync(AuthenticationProperties properties)
        {
            return base.HandleForbiddenAsync(properties);
        }
    }
}
