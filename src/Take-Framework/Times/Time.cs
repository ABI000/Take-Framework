namespace TakeFramework.Times
{
    public class Time : ITime
    {
        public DateTime Now => DateTime.Now;

        public DateTime UtcNow => DateTime.UtcNow;

        public DateTime GetLocalTimeDateTime(DateTime dateTime)
        {
            //todo:获取Http上下文


            return dateTime.ToLocalTime();
        }
    }
}
