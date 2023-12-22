using TakeFramework.EventBus;

namespace Sample.Server;

public class BlogIntegrationEvent : IntegrationEvent
{
    public BlogIntegrationEvent(string title) { Title = title; }
    public string Title { get; set; }
}
