using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using TakeFramework.JWT;

namespace TakeFramework.Web.Authorization
{
    /// <summary>
    /// 进一步验证用户权限
    /// </summary>
    public class TFAuthorizationHandler : AuthorizationHandler<TFPolicyRequirement>
    {
        public TFAuthorizationHandler()
        {

        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, TFPolicyRequirement requirement)
        {
            //处理权限

            await Task.CompletedTask;
        }
    }
}
