using System.Security.Claims;

namespace TakeFramework.Authorization
{
    public class BasePayLoad : IPayLoad
    {
        public BasePayLoad(ClaimsPrincipal claimsPrincipal)
        {
            GetPayLoad(claimsPrincipal);
        }
        public string Account { get; set; }
        public string RealName { get; set; }
        public string UserId { get; set; }
        public string System { get; set; }
        public string Authentication { get; set; }
        public virtual void GetPayLoad(ClaimsPrincipal claimsPrincipal)
        {
            this.UserId = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.SerialNumber)?.Value ?? string.Empty;
            this.Account = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Upn)?.Value ?? string.Empty;
            this.RealName = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value ?? string.Empty;

            this.System = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.System)?.Value ?? string.Empty;
            this.Authentication = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Authentication)?.Value ?? string.Empty;
        }
        public virtual ClaimsPrincipal SetIdentity(string authenticationType = "")
        {
            List<Claim> claim = new()
            {
                { new Claim(ClaimTypes.SerialNumber, this.UserId ?? string.Empty) },
                { new Claim(ClaimTypes.Upn, this.Account ?? string.Empty) },
                { new Claim(ClaimTypes.Name, this.RealName ?? string.Empty) },

                { new Claim(ClaimTypes.Authentication, this.Authentication ?? string.Empty) },
                { new Claim(ClaimTypes.System, this.System ?? string.Empty) },
            };
            ClaimsIdentity claimsIdentity = new(claim, authenticationType);
            ClaimsPrincipal claimsPrincipal = new(claimsIdentity);
            return claimsPrincipal;
        }
    }
}
