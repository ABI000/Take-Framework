using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sample.Server;
using Sample.Server.Contracts;
using TakeFramework.Cache;
using TakeFramework.Web;

namespace Sample.Host.Shared.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ICacheProvider cacheProvider;
        private readonly TestService testService;
        public TestController(TestService testService, CacheProviderFactory cacheProviderFactory)
        {
            this.testService = testService;
            cacheProvider = cacheProviderFactory.GetCacheProvider();
            //初始化翻译

            cacheProvider.Add("LocalizationResource_zh-CN", new List<KeyValuePair<string, string>> {
                new KeyValuePair<string, string>("ServerError", "服务错误")
            });
        }


        [HttpGet("userList")]
        public IEnumerable<UserDto> userList()
        {
            return testService.List();
        }
        [HttpGet("GetException")]
        public void GetException()
        {
            testService.GetException();
        }

        [HttpGet("GetCache")]
        public ApiResponse GetCache(string key, string value)
        {
            var output = cacheProvider.Get(key);
            if (output is null)
            {
                output = value;
                cacheProvider.Add(key, value);

            }
            return new ApiResponse<string>(output.ToString());
        }
    }

}