namespace TakeFramework.EntityFrameworkCore
{
    public class DBSettings
    {
        public const string Position = "DBSettings";

        public required List<DBSetting> DBSettingList { get; set; }
    }

    public class DBSetting
    {
        public required string ConnectionString { get; set; }
        public required string Name { get; set; }
        public bool IsDefault { get; set; }
    }
}
