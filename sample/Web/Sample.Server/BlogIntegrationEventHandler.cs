using TakeFramework.EventBus;

namespace Sample.Server;

public class BlogIntegrationEventHandler : IIntegrationEventHandler<BlogIntegrationEvent>
{
    public Task Handle(BlogIntegrationEvent @event)
    {
        Console.WriteLine($"{nameof(BlogIntegrationEventHandler)}:Id:{@event.Id},Title:{@event.Title}");
        return Task.CompletedTask;
    }
}
