using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Sample.Host.Shared.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController(ILogger<UserController> logger) : ControllerBase
    {
        private readonly ILogger<UserController> _logger = logger;

        [HttpGet("GetException")]
        public string GetException()
        {
            throw new NotImplementedException();
        }
    }
}