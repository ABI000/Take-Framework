using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sample.Core;
using Sample.Server;

namespace Sample.Host.Shared.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;

        private readonly UserService userService;
        public UserController(ILogger<UserController> logger, IMapper mapper, UserService userService)
        {
            this.userService = userService;
            _mapper = mapper;
            _logger = logger;
        }


        [HttpGet("userList")]
        public IEnumerable<User> userList()
        {
            return userService.List();
        }
    }

}