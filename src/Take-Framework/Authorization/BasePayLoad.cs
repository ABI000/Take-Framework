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
        public string UserName { get; set; }
        public string RealName { get; set; }
        public long UserId { get; set; }

        public virtual void GetPayLoad(IEnumerable<Claim> claims)
        {
            UserName = claims?.FirstOrDefault(x => x.Type == "Username")?.Value ?? "";
            RealName = claims?.FirstOrDefault(x => x.Type == "RealName")?.Value ?? "";
            _ = long.TryParse(claims?.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti)?.Value, out long userId);
            UserId = userId;
        }
    }
}
