using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TakeFramework.JWT
{

    public class JwtHelper(IOptions<JwtConfiguration> configuration) : IJwt
    {
        private readonly JwtConfiguration configuration = configuration.Value;

        public string GenerateToken(params Claim[] claims)
        {
            var jwtSecurityToken = new JwtSecurityToken(
                configuration.Issuer,     //Issuer
                configuration.Audience,   //Audience
                claims,                          //Claims,
                DateTime.UtcNow,                    //notBefore
                DateTime.UtcNow.AddMinutes(int.Parse(configuration.Expires)),    //expires
                new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.SecretKey)), configuration.Algorithm)//Credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }
        public async Task<string> GenerateTokenAsync(params Claim[] claims)
        {
            return await Task.Run(() => { return this.GenerateToken(claims); });
        }

        public (bool, ClaimsPrincipal) ValidateToken(string token)
        {
            if (token == null)
            {
                return (false, default!);
            }
            var handler = new JwtSecurityTokenHandler();
            //判断token
            if (!handler.CanReadToken(token))
            {
                return (false, default!);
            }
            var claimsPrincipal = handler.ValidateToken(token, GetTokenValidationParameters(), out SecurityToken validatedToken);
            return (true, claimsPrincipal);
        }
        public async Task<(bool, ClaimsPrincipal)> ValidateTokenAsync(string token)
        {
            if (token == null)
            {
                return (false, default!);
            }
            var handler = new JwtSecurityTokenHandler();
            //判断token
            if (!handler.CanReadToken(token))
            {
                return (false, default!);
            }
            TokenValidationResult tokenValidationResult = await handler.ValidateTokenAsync(token, GetTokenValidationParameters());
            return (tokenValidationResult.IsValid, new ClaimsPrincipal(tokenValidationResult.ClaimsIdentity));
        }
        private TokenValidationParameters GetTokenValidationParameters()
        {
            return new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.SecretKey)),
                ValidateIssuer = configuration.ValidateIssuer,
                ValidateAudience = configuration.ValidateAudience,
                ClockSkew = TimeSpan.Zero,
            };

        }
    }
}
