using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TakeFramework.Cache;
using TakeFramework.SemanticKernel;
using TakeFramework.Web;

namespace Sample.Host.Shared.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ICacheProvider cacheProvider;
        private readonly SemanticKernelService semanticKernelService;
        public TestController(CacheProviderFactory cacheProviderFactory, SemanticKernelService semanticKernelService)
        {
            cacheProvider = cacheProviderFactory.GetCacheProvider();
            //��ʼ������

            cacheProvider.Add("LocalizationResource_zh-CN", new List<KeyValuePair<string, string>> {
                new("ServerError", "�������")
            });
            this.semanticKernelService = semanticKernelService;
            this.semanticKernelService=semanticKernelService;
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
        [Authorize]
        [AllowAnonymous()]
        [HttpGet("GetAllowAnonymous")]
        public ApiResponse GetAllowAnonymous()
        {
            return new ApiResponse<string>("ok");
        }
       
        [AllowAnonymous()]
        [HttpGet("semanticKernelServiceTest")]
        public async Task semanticKernelServiceTest(string prompt)
        {
           await  semanticKernelService.Test(prompt);
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