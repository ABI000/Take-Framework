using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using TakeFramework.JWT;

namespace TakeFramework.Web.Authentication
{
    /// <summary>
    /// 验证用户身份是否符合当前SchemeName的认证逻辑
    /// </summary>
    public class TFAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IJwt jwt;
        public const string SchemeName = "TF";

        public TFAuthenticationHandler(IJwt jwt,
            IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
            this.jwt = jwt;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            Request.Headers.TryGetValue("Authorization", out StringValues values);
            string valStr = values.ToString();
            if (!string.IsNullOrWhiteSpace(valStr))
            {
                (bool check, ClaimsPrincipal claimsPrincipal) = await jwt.ValidateTokenAsync(valStr);
                if (!check)
                    return AuthenticateResult.Fail("未登陆");
                else
                {
                    return AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, SchemeName));
                }
            }
            return AuthenticateResult.Fail("未登陆");
        }
    }
}
