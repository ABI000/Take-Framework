using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

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
