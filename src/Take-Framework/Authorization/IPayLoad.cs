using System.Security.Claims;

namespace TakeFramework.Authorization
{
    public interface IPayLoad
    {
        public void GetPayLoad(ClaimsPrincipal claimsPrincipal);
        public ClaimsPrincipal SetIdentity(string authenticationType = "");
    }
}
