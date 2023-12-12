using Castle.DynamicProxy;
using TakeFramework.Cache;

namespace TakeFramework;

public class CacheIInterceptor(CacheProviderFactory cacheProviderFactory) : IInterceptor
{
    private readonly CacheProviderFactory _cacheProviderFactory = cacheProviderFactory;

    public void Intercept(IInvocation invocation)
    {
        CacheAttribute? attribute = invocation.MethodInvocationTarget.GetCustomAttributes(typeof(CacheAttribute), false).FirstOrDefault() as CacheAttribute;
        if (attribute is null)
        {
            invocation.Proceed();
        }
        else
        {
            try
            {
                invocation.Proceed();
                var cacheProvider = _cacheProviderFactory.GetCacheProviderByKey(attribute.Key);
                cacheProvider.Add(attribute.CacheKey, invocation.ReturnValue);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
