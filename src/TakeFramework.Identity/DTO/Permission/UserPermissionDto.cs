namespace TakeFramework.Identity.DTO
{
    public class UserPermissionDto
    {
        /// <summary>
        /// 用户声明
        /// </summary>
        public Dictionary<string, string> UserClaims { get; set; }
        /// <summary>
        /// 角色声明
        /// </summary>
        public Dictionary<string, Dictionary<string, string>> RoleClaims { get; set; }

        /// <summary>
        /// 所有声明
        /// 合并用户、角色声明
        /// </summary>
        public Dictionary<string, string> AllClaims { get; set; }
    }
}
