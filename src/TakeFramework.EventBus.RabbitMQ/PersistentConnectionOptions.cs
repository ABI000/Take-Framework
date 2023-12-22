namespace TakeFramework.EventBus.RabbitMQ;

public class PersistentConnectionOptions
{
    public const string Position = nameof(PersistentConnectionOptions);

    public string HostName { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public int RetryCount { get; set; }
    public string SubscriptionClientName{get;set;}
    

}
