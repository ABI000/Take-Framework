using Microsoft.AspNetCore.Identity;
using TakeFramework.Identity.PO;

namespace TakeFramework.Identity
{
    public class UserManager: IUserManager
    {
        private readonly IUserRepository _userRepository;

        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> GetUserByIdAsync(long id)
        {
            return await _userRepository.FistOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User?> GetUserByUserNameAsync(string userName)
        {
            return await _userRepository.FistOrDefaultAsync(x => x.UserName == userName);
        }

    }
}
