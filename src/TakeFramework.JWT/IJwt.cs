using System.Security.Claims;

namespace TakeFramework.JWT
{
    public interface IJwt
    {
        public string GenerateToken(params Claim[] claims);
        public Task<string> GenerateTokenAsync(params Claim[] claims);
        public (bool, ClaimsPrincipal) ValidateToken(string token);
        public Task<(bool, ClaimsPrincipal)> ValidateTokenAsync(string token);
    }
}
