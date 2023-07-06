using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sample.Core;
using Sample.Server;
using Sample.Server.Contracts;
using System.Runtime.CompilerServices;
using TakeFramework.Cache;
using TakeFramework.Exceptions;
using TakeFramework.Web;

namespace Sample.Host.Shared.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly ICacheProvider cacheProvider;
        private readonly UserService userService;
        public TestController(ILogger<UserController> logger, UserService userService, CacheProviderFactory cacheProviderFactory)
        {
            this.userService = userService;
            _logger = logger;
            cacheProvider = cacheProviderFactory.GetCacheProvider();
        }


        [HttpGet("userList")]
        public IEnumerable<UserDto> userList()
        {
            return userService.List();
        }
        [HttpGet("GetException")]
        public string GetException()
        {
            throw new NotImplementedException();
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