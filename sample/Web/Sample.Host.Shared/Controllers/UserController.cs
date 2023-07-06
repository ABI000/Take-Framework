using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sample.Core;
using Sample.Server;
using Sample.Server.Contracts;
using TakeFramework.Exceptions;

namespace Sample.Host.Shared.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        private readonly UserService userService;
        public UserController(ILogger<UserController> logger, UserService userService)
        {
            this.userService = userService;
            _logger = logger;
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
    }
}