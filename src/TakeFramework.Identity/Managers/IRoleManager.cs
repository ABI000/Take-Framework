using TakeFramework.Identity.DTO;

namespace TakeFramework.Identity.Managers
{
    public interface IRoleManager
    {
        #region 查询


        /// <summary>
        /// 根据Id查找角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<UserDto?> GetUserByIdAsync(long id);
        /// <summary>
        /// 根据角色名查找角色
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public Task<UserDto?> GetUserByUserNameAsync(string userName);
        #endregion

        #region  编辑

        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<UserDto?> CreateAsync(UserDto po);
        /// <summary>
        /// 修改角色信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public Task<UserDto?> UpdateAsync(UserDto po);
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        public Task DeleteAsync(UserDto userDto);
        #endregion


        #region 授权/撤权

        #endregion

    }
}
