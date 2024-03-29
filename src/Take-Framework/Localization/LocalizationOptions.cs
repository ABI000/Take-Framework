﻿namespace TakeFramework.Localization
{
    public class LocalizationOptions
    {
        public const string Position = nameof(LocalizationOptions);
        /// <summary>
        /// 存储类型,DB,File,Cache
        /// </summary>
        public string StorageType { get; set; } = "DB";
        /// <summary>
        /// 本地化存储位置
        /// </summary>
        public string? Storage { get; set; }
        /// <summary>
        /// 默认语言code
        /// </summary>
        public string DefaultLanguageCode { get; set; } = "en-US";
    }
}
