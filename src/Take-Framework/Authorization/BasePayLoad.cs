using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Security.Claims;

namespace TakeFramework.Authorization
{
    public class BasePayLoad
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        public BasePayLoad(HttpContext httpContext)
        {
            GetPayLoad(httpContext.User.Claims);
        }
        public string Account { get; set; }
        public string RealName { get; set; }
        public string UserId { get; set; }
        public string System { get; set; }
        public string Authentication { get; set; }
        public virtual void GetPayLoad(IEnumerable<Claim> claims)
        {
            this.UserId = claims.FirstOrDefault(x => x.Type == ClaimTypes.SerialNumber)?.Value ?? string.Empty;
            this.Account = claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
            this.RealName = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value ?? string.Empty;

            this.System = claims.FirstOrDefault(x => x.Type == ClaimTypes.System)?.Value ?? string.Empty;
            this.Authentication = claims.FirstOrDefault(x => x.Type == ClaimTypes.Authentication)?.Value ?? string.Empty;
        }
        /// <summary>
        /// 将信息转成ClaimsIdentity并写入HttpContext.User
        /// </summary>
        /// <param name="context">HttpContext</param>
        /// <returns>AuthenticationTicket</returns>
        public AuthenticationTicket SetIdentity(string authenticationType)
        {
            List<Claim> claim = new()
            {
                { new Claim(ClaimTypes.SerialNumber, this.UserId ?? string.Empty) },
                { new Claim(ClaimTypes.NameIdentifier, this.Account ?? string.Empty) },
                { new Claim(ClaimTypes.System, this.System ?? string.Empty) }
            };
            ClaimsIdentity claimsIdentity = new(claim, authenticationType);
            ClaimsPrincipal claimsPrincipal = new(claimsIdentity);
            AuthenticationTicket ticket = new(claimsPrincipal, string.Empty);
            return ticket;
        }
    }
}
