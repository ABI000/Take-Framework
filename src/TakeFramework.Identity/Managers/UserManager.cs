using TakeFramework.Domain.Entities;
using TakeFramework.Identity.DTO;
using TakeFramework.Identity.PO;

namespace TakeFramework.Identity.Managers
{
    public class UserManager<TUser, TUserDto, TPrimaryKey> : IUserManager<TUser, TUserDto>
        where TUser : User, IEntity<TPrimaryKey>
        where TUserDto : class, new()
    {
        private readonly IUserRepository<TUser, TPrimaryKey> _userRepository;

        public UserManager(IUserRepository<TUser, TPrimaryKey> userRepository)
        {
            _userRepository = userRepository;
        }

        public Task ChangePassWord(ChangePassWordDto input)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto?> CreateAsync(UserDto po)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(UserDto userDto)
        {
            throw new NotImplementedException();
        }

        public async Task<TUser?> GetUserByIdAsync(long id)
        {
            return await _userRepository.FistOrDefaultAsync(x => x.Id == id);
        }

        public async Task<TUser?> GetUserByUserNameAsync(string userName)
        {
            return await _userRepository.FistOrDefaultAsync(x => x.UserName == userName);
        }

        public Task<LoginOutputDto?> Login(LoginDto id)
        {
            throw new NotImplementedException();
        }

        public Task LogOut(UserDto userDto)
        {
            throw new NotImplementedException();
        }

        public Task LogOut(TUserDto userDto)
        {
            throw new NotImplementedException();
        }

        public Task RecoverPassword(RecoverPasswordDto input)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto?> UpdateAsync(UserDto po)
        {
            throw new NotImplementedException();
        }



        Task<TUserDto?> IUserManager<TUser, TUserDto>.GetUserByIdAsync(long id)
        {
            throw new NotImplementedException();
        }
        Task<TUserDto?> IUserManager<TUser, TUserDto>.GetUserByUserNameAsync(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
