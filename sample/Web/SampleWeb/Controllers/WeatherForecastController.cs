using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace SampleWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMapper _mapper;

        private readonly UserService userService;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMapper mapper, UserService userService)
        {
            this.userService = userService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("mapper")]
        public bool mapper()
        {
            var test = new Test1 { Id = 1 };
            var ss = _mapper.Map<Test1, Test2>(test);
            return ss.Id == test.Id;
        }
        [HttpGet("userList")]
        public IEnumerable<User> userList()
        {
            return userService.List();
        }
    }

    public class Test1
    {
        public int Id { get; set; }

    }

    public class Test2
    {
        public int Id { get; set; }

    }

}