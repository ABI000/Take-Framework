namespace TakeFramework.Times
{
    public interface ITime
    {
        public DateTime Now { get; }

        public DateTime UtcNow { get; }

        public DateTime GetLocalTimeDateTime(DateTime dateTime);
    }
}
