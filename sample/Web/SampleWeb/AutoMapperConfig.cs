using AutoMapper;
using SampleWeb.Controllers;

namespace SampleWeb
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Test1, Test2>();
        }
    }
}
