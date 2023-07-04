using AutoMapper;
using Sample.Core;
using Sample.Server.Contracts;

namespace Sample.Server
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}
