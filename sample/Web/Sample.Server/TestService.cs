using AutoMapper;
using Sample.Core;
using Sample.Domain;
using Sample.Domain.Shared;
using Sample.Server.Contracts;
using System.Security.Claims;
using TakeFramework.Domain.Services;
using TakeFramework.Domain.Uow;
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
        [UnitOfWork]
        public async Task CreateAsync(UserDto userDto)
        {
            await rpository.CreateAsync(mapper.Map<UserDto, User>(userDto));
            GetException();
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
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public TreeO GetTreeO()
        {
            var output = new TreeO();

            output = (TreeO)output.GenerateTree(new List<TreeO>
            {
                 new TreeO() { Id="0"},
                 new TreeO() { Id="1",ParentId="0"},
                 new TreeO() { Id="2",ParentId="1"}
            });

            return output;
        }
    }
}