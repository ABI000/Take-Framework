using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using TakeFramework.Web.Authentication;

namespace TakeFramework.Web.Authorization
{
    /// <summary>
    /// TFOpenApi 配置下的策略权限处理模块，确定权限验证所需参数和所要去到的AuthorizationHandler（权限处理程序）
    /// </summary>
    public class TFAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) : IAuthorizationPolicyProvider
    {
        public DefaultAuthorizationPolicyProvider FallbackPolicyProvider { get; } = new DefaultAuthorizationPolicyProvider(options);

        public const string POLICY_PREFIX = "TFOpenApi";

        public Task<AuthorizationPolicy> GetDefaultPolicyAsync() => FallbackPolicyProvider.GetDefaultPolicyAsync();

        public Task<AuthorizationPolicy?> GetFallbackPolicyAsync() => FallbackPolicyProvider.GetFallbackPolicyAsync();

        public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            if (policyName.Equals(POLICY_PREFIX, StringComparison.OrdinalIgnoreCase))
            {
                var policy = new AuthorizationPolicyBuilder(TFAuthenticationHandler.SchemeName);
                policy.AddRequirements(new TFPolicyRequirement());
                return Task.FromResult(policy?.Build());
            }
            return Task.FromResult<AuthorizationPolicy>(null);
        }
    }
}
