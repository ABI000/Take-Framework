using TakeFramework.Identity.DTO;

namespace TakeFramework.Identity.Permission
{
    public interface IPermissionProvider
    {
        #region 用户权限
        /// <summary>
        /// 根据用户id获取用户权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<UserPermissionDto> GetUserPermissionByUserId(long userId);
        /// <summary>
        /// 根据用户名获取用户权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<UserPermissionDto> GetUserPermissionByUserName(string userName);
        /// <summary>
        /// 根据用户Id获取用户声明
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<Dictionary<string, string>> GetUserClaimByUserId(long userId);
        /// <summary>
        /// 根据用户名获取用户声明
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<Dictionary<string, string>> GetUserClaimByUserName(string userName);
        /// <summary>
        /// 根据用户Id获取用户声明（不包含角色声明）
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<Dictionary<string, string>> GetUserClaimExcludeRoleByUserId(long userId);
        /// <summary>
        /// 根据用户名获取用户声明（不包含角色声明）
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<Dictionary<string, string>> GetUserClaimExcludeRoleByUserName(string userName);
        #endregion

        #region 角色
        /// <summary>
        /// 根据角色Id获取用户声明
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<Dictionary<string, string>> GetRoleClaimByUserIdWithRoleId(long userid, long roleId);
        /// <summary>
        /// 根据角色名获取用户声明
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<Dictionary<string, string>> GetRoleClaimByUserIdWithRoleName(long userid, string roleName);

        /// <summary>
        /// 根据角色Id获取用户声明
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<Dictionary<string, string>> GetRoleClaimByUserNameWithRoleId(string userName, long roleId);
        /// <summary>
        /// 根据角色名获取用户声明
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<Dictionary<string, string>> GetRoleClaimByUserNameWithRoleName(string userName, string roleName);
        #endregion
    }
}
