using Sample.Core;
using Sample.Domain;
using TakeFramework.Domain.Services;

namespace Sample.Server
{
    public class UserService : BaseService
    {
        protected readonly UserRepository rpository;

        public UserService(UserRepository rpository)
        {
            this.rpository = rpository;
        }

        public List<User> List()
        {
            return rpository.List();
        }
    }
}