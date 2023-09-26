using TakeFramework.Identity.DTO;

namespace TakeFramework.Identity.Managers
{
    public interface IUserManager<TUser,TUserDto>
        where TUser : class
    {
        #region 查询


        /// <summary>
        /// 根据Id查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<TUserDto?> GetUserByIdAsync(long id);
        /// <summary>
        /// 根据用户名查找用户
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public Task<TUserDto?> GetUserByUserNameAsync(string userName);
        #endregion

        #region  编辑

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<UserDto?> CreateAsync(UserDto po);
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public Task<UserDto?> UpdateAsync(UserDto po);
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        public Task DeleteAsync(UserDto userDto);
        #endregion

        /// <summary>
        /// 登录接口
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<LoginOutputDto?> Login(LoginDto id);


        /// <summary>
        /// 登出，需要自行实现
        /// jwt是无状态的，所以需要额外增加模块进行状态附加
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task LogOut(TUserDto userDto);


        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        public Task ChangePassWord(ChangePassWordDto input);


        /// <summary>
        /// 找回密码
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        public Task RecoverPassword(RecoverPasswordDto input);

    }
}
