using TakeFramework.Domain.Entities;
using TakeFramework.Domain.Services;

namespace SampleWeb
{
    public class UserService : BaseService
    {
        protected readonly UserRepository rpository;

        public UserService( UserRepository rpository)
        {
            this.rpository = rpository;
        }

        public List<User> List()
        {
            return rpository.List();
        }
    }
}