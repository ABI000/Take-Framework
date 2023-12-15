using AutoMapper;
using Sample.Domain;
using Sample.Server.Contracts;

namespace Sample.Server
{
    public class BlogProfile : Profile
    {
        public BlogProfile()
        {
            CreateMap<Blog, BlogDto>();
            CreateMap<BlogDto, Blog>();
        }
    }
}
