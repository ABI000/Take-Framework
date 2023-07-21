using AutoMapper;
using Sample.Core;
using Sample.Domain;
using Sample.Server.Contracts;
using TakeFramework.Domain.Services;

namespace Sample.Server
{
    public class UserService : BaseService
    {
        protected readonly UserRepository rpository;

        protected readonly IMapper mapper;
        public UserService(UserRepository rpository, IMapper mapper)
        {
            this.mapper = mapper;
            this.rpository = rpository;
        }
        public List<UserDto> List()
        {
            return mapper.Map<List<User>, List<UserDto>>(rpository.List());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public TreeO GetTreeO()
        {
            var output = new TreeO();

            output.GenerateTree(new List<TreeO>
            {
                 new TreeO() { Id="0"},
                 new TreeO() { Id="1",ParentId="0"},
                 new TreeO() { Id="2",ParentId="1"}
            });
            output.ChildList.ToList();
            return output;
        }
    }
}