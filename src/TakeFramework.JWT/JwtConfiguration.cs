namespace TakeFramework.JWT
{
    public class JwtConfiguration
    {
        public const string Position = "JwtConfiguration";

        /// <summary>
        /// 颁发者
        /// </summary>
        public string Issuer { get; set; }
        /// <summary>
        /// 是否校验颁发者
        /// </summary>
        public bool ValidateIssuer { get; set; } = false;
        /// <summary>
        /// 受众
        /// </summary>
        public string Audience { get; set; }
        /// <summary>
        /// 是否校验受众
        /// </summary>
        public bool ValidateAudience { get; set; }=false;
        /// <summary>
        /// 过期时间
        /// </summary>
        public string Expires { get; set; }
        /// <summary>
        /// 密钥
        /// </summary>
        public string SecretKey { get; set; }
        /// <summary>
        /// 算法
        /// </summary>
        public string Algorithm { get; set; } = "HS256";
    }
}
