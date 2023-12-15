using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TakeFramework.Cache;
using TakeFramework.Web;

namespace Sample.Host.Shared.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ICacheProvider cacheProvider;
        public TestController(CacheProviderFactory cacheProviderFactory)
        {
            cacheProvider = cacheProviderFactory.GetCacheProvider();
            //��ʼ������

            cacheProvider.Add("LocalizationResource_zh-CN", new List<KeyValuePair<string, string>> {
                new("ServerError", "�������")
            });
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
        [HttpGet("GetApiResponse")]
        public ApiResponse GetApiResponse()
        {
            return new ApiResponse<TestApiResponse>(new TestApiResponse());
        }
        [HttpPost("PostApiResponse")]
        public ApiResponse PostApiResponse(TestApiResponse intput)
        {
            return new ApiResponse<TestApiResponse>(intput);
        }

        [Authorize]
        [HttpGet("GetAuthorization")]
        public ApiResponse GetAuthorization()
        {
            return new ApiResponse<string>("ok");
        }

    }
    public class TestApiResponse
    {
        public DateTimeOffset DateTimeOffset { get; set; } = DateTimeOffset.MinValue;
        public DateTime DateTime { get; set; } = DateTime.Now;
        public string Name { get; set; } = "Test";
        public string? NullName { get; set; } = null;
        public int IntNmb { get; set; } = 1;
        public decimal Decimal { get; set; } = 231.123M;
        public double Double { get; set; } = 123.1231123;
        public long Long { get; set; } = 123456789012345;
        public float Float { get; set; } = 123.1231123F;
    }

}