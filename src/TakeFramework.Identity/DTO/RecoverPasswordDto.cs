namespace TakeFramework.Identity.DTO
{
    public class RecoverPasswordDto: ChangePassWordDto
    {
        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string Captcha { get; set; }
    }
}
