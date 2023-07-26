namespace TakeFramework.EntityFrameworkCore
{
    public class DBSettings
    {
        public const string Position = "DBSettings";

        public List<DBSetting> DBSettingList { get; set; }
    }

    public class DBSetting
    {
        public string ConnectionString { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }
    }
}
