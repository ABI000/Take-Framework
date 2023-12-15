using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sample.Server.Contracts;

namespace Sample.Host.Shared.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogController(ILogger<BlogController> logger, IBlogService blogService) : ControllerBase
    {
        private readonly ILogger<BlogController> _logger = logger;
        private readonly IBlogService _blogService = blogService;
        [HttpGet("GetException")]
        public string GetException()
        {
            throw new NotImplementedException();
        }
        [HttpPost("Create")]
        // [AllowAnonymous]
        public Task<BlogDto> CreateAsync(BlogDto input)
        {
            return _blogService.CreateAsync(input);
        }


    }
}