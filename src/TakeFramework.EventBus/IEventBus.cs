namespace TakeFramework.EventBus;

public interface IEventBus
{
    Task PublishAsync(IntegrationEvent @event);

    Task SubscribeAsync<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>;

    Task SubscribeDynamicAsync<TH>(string eventName) where TH : IDynamicIntegrationEventHandler;

    void UnsubscribeDynamic<TH>(string eventName) where TH : IDynamicIntegrationEventHandler;

    void Unsubscribe<T, TH>() where TH : IIntegrationEventHandler<T> where T : IntegrationEvent;
}

