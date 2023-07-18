using AutoMapper;
using Sample.Core;
using Sample.Domain;
using Sample.Domain.Shared;
using Sample.Server.Contracts;
using System.Security.Claims;
using TakeFramework.Domain.Services;
using TakeFramework.Exceptions;
using TakeFramework.JWT;

namespace Sample.Server
{
    public class TestService : BaseService
    {
        protected readonly UserRepository rpository;
        protected readonly IJwt jwt;
        protected readonly IMapper mapper;
        public TestService(UserRepository rpository, IMapper mapper, IJwt jwt)
        {
            this.jwt = jwt;
            this.mapper = mapper;
            this.rpository = rpository;
        }
        public List<UserDto> List()
        {
            return mapper.Map<List<User>, List<UserDto>>(rpository.List());
        }

        public void GetException()
        {
            throw new BusinessException(ErrorCodes.ServerError, ErrorCodes.ServerErrorCode);
        }
        public async Task<string> GenerateTokenAsync()
        {
            return await jwt.GenerateTokenAsync(
                new Claim(ClaimTypes.SerialNumber, "1"),
                new Claim(ClaimTypes.Upn, "admin@admin.com"),
                new Claim(ClaimTypes.Name, "admin"),
                new Claim(ClaimTypes.System, "admin"));
        }
    }
}