namespace TakeFramework.Identity.DTO
{
    public class ChangePassWordDto
    {
        /// <summary>
        /// 新的密码
        /// </summary>
        public string NewPassWord { get; set; }
        /// <summary>
        /// 二次确认
        /// </summary>
        public string ConfirmPassWord { get; set; }
    }
}
