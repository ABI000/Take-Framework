using System.Net.Sockets;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System.IO;
using RabbitMQ.Client.Events;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace TakeFramework.EventBus.RabbitMQ;
/// <summary>
/// 
/// </summary>
public class EventBusRabbitMQ : IEventBus, IDisposable
{
    const string BROKER_NAME = "eshop_event_bus";
    const string AUTOFAC_SCOPE_NAME = "eshop_event_bus";
    private readonly IPersistentConnection _persistentConnection;
    private readonly IEventBusSubscriptionsManager _subsManager;
    private string _queueName;
    private IChannel _consumerChannel;
    private readonly int _retryCount;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<EventBusRabbitMQ> _logger;
    private readonly PersistentConnectionOptions _options;
    public EventBusRabbitMQ(IPersistentConnection persistentConnection, ILogger<EventBusRabbitMQ> logger, IEventBusSubscriptionsManager subsManager, IServiceScopeFactory scopeFactory, IOptions<PersistentConnectionOptions> options)
    {
        _options = options.Value ?? throw new ArgumentNullException(nameof(logger));
        _logger = logger;
        _persistentConnection = persistentConnection;
        _subsManager = subsManager ?? new InMemoryEventBusSubscriptionsManager();
        _queueName = _options.SubscriptionClientName;

        _scopeFactory = scopeFactory;
        _retryCount = _options.RetryCount;
    }
    public async Task InitAsync()
    {
        _consumerChannel = await CreateConsumerChannelAsync();

        _subsManager.OnEventRemoved += OnEventRemovedHandler;
    }
    /// <summary>
    /// 解除事件订阅实际执行方法,触发本地事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="eventName"></param>
    private void OnEventRemovedHandler(object? sender, string eventName)
    {
        _ = SubsManager_OnEventRemovedAsync(sender, eventName)
            .ContinueWith(t =>
            {
                if (t.Exception != null)
                    _logger.LogError(t.Exception, "OnEventRemovedAsync error");
            }, TaskContinuationOptions.OnlyOnFaulted);
    }

    /// <summary>
    /// 解除事件订阅实际执行方法
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="eventName"></param>
    private async Task SubsManager_OnEventRemovedAsync(object? sender, string eventName)
    {
        CheckConnect();
        using var channel = await _persistentConnection.CreateChannelAsync();
        await channel.QueueUnbindAsync(queue: _queueName,
             exchange: BROKER_NAME,
             routingKey: eventName);

        if (_subsManager.IsEmpty)
        {
            _queueName = string.Empty;
            await _consumerChannel.CloseAsync();
        }
    }

    private async Task<IChannel> CreateConsumerChannelAsync()
    {
        CheckConnect();

        _logger.LogTrace("Creating RabbitMQ consumer channel");

        var channel = await _persistentConnection.CreateChannelAsync();

        await channel.ExchangeDeclareAsync(exchange: BROKER_NAME,
                                 type: "direct");

        await channel.QueueDeclareAsync(queue: _queueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

        channel.CallbackExceptionAsync += async (sender, ea) =>
        {
            _logger.LogWarning(ea.Exception, "Recreating RabbitMQ consumer channel");

            _consumerChannel.Dispose();
            _consumerChannel = await CreateConsumerChannelAsync();
            StartBasicConsume();
        };

        return channel;
    }

    public async Task PublishAsync(IntegrationEvent @event)
    {
        CheckConnect();

        var policy = RetryPolicy.Handle<BrokerUnreachableException>()
           .Or<SocketException>()
           .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
           {
               _logger.LogWarning(ex, "Could not publish event: {EventId} after {Timeout}s ({ExceptionMessage})", @event.Id, $"{time.TotalSeconds:n1}", ex.Message);
           });

        var eventName = @event.GetType().Name;

        _logger.LogTrace("Creating RabbitMQ channel to publish event: {EventId} ({EventName})", @event.Id, eventName);

        using var channel = await _persistentConnection.CreateChannelAsync();
        _logger.LogTrace("Declaring RabbitMQ exchange to publish event: {EventId}", @event.Id);

        await channel.ExchangeDeclareAsync(exchange: BROKER_NAME, type: "direct");

        var body = JsonSerializer.SerializeToUtf8Bytes(@event, @event.GetType(), new JsonSerializerOptions
        {
            WriteIndented = true
        });
        await policy.Execute(async () =>
                {
                    // 替换原有的 CreateBasicProperties 调用，使用 RabbitMQ.Client.BasicProperties
                    // 需要添加 using RabbitMQ.Client; 并直接 new BasicProperties()

                    var properties = new BasicProperties
                    {
                        DeliveryMode = DeliveryModes.Persistent // persistent
                    };

                    _logger.LogTrace("Publishing event to RabbitMQ: {EventId}", @event.Id);

                    await channel.BasicPublishAsync(
                         exchange: BROKER_NAME,
                         routingKey: eventName,
                         mandatory: true,
                         basicProperties: properties,
                         body: body);
                });
    }

    /// <summary>
    /// 检查连接
    /// </summary>
    private void CheckConnect()
    {
        if (!_persistentConnection.IsConnected)
        {
            _persistentConnection.TryConnect();
        }
    }
    /// <summary>
    /// 订阅
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TH"></typeparam>
    public async Task SubscribeAsync<T, TH>()
        where T : IntegrationEvent
        where TH : IIntegrationEventHandler<T>
    {
        var eventName = _subsManager.GetEventKey<T>();
        _logger.LogInformation("Subscribing to event {EventName} with {EventHandler}", eventName, typeof(TH).GetGenericTypeName());
        await DoInternalSubscriptionAsync(eventName);
        _subsManager.AddSubscription<T, TH>();
        StartBasicConsume();
    }
    /// <summary>
    /// 订阅动态内容
    /// </summary>
    /// <typeparam name="TH"></typeparam>
    /// <param name="eventName"></param>
    public async Task SubscribeDynamicAsync<TH>(string eventName) where TH : IDynamicIntegrationEventHandler
    {
        _logger.LogInformation("Subscribing to dynamic event {EventName} with {EventHandler}", eventName, typeof(TH).GetGenericTypeName());

        await DoInternalSubscriptionAsync(eventName);
        _subsManager.AddDynamicSubscription<TH>(eventName);
        StartBasicConsume();
    }
    /// <summary>
    /// 启动消费
    /// </summary>
    private void StartBasicConsume()
    {
        _logger.LogTrace("Starting RabbitMQ basic consume");

        if (_consumerChannel != null)
        {
            var consumer = new AsyncEventingBasicConsumer(_consumerChannel);

            consumer.ReceivedAsync += Consumer_ReceivedAsync;

            _consumerChannel.BasicConsumeAsync(
                queue: _queueName,
                autoAck: false,
                consumer: consumer);
        }
        else
        {
            _logger.LogError("StartBasicConsume can't call on _consumerChannel == null");
        }
    }
    /// <summary>
    /// 消费订阅事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="event"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    private async Task Consumer_ReceivedAsync(object sender, BasicDeliverEventArgs eventArgs)
    {
        var eventName = eventArgs.RoutingKey;
        var message = Encoding.UTF8.GetString(eventArgs.Body.Span);

        try
        {
            if (message.Contains("throw-fake-exception", StringComparison.InvariantCultureIgnoreCase))
            {
                throw new InvalidOperationException($"Fake exception requested: \"{message}\"");
            }

            await ProcessEventAsync(eventName, message);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "----- ERROR Processing message \"{Message}\"", message);
        }

        // Even on exception we take the message off the queue.
        // in a REAL WORLD app this should be handled with a Dead Letter Exchange (DLX). 
        // For more information see: https://www.rabbitmq.com/dlx.html
        await _consumerChannel.BasicAckAsync(eventArgs.DeliveryTag, multiple: false);
    }

    /// <summary>
    /// 执行事件
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    private async Task ProcessEventAsync(string eventName, string message)
    {
        _logger.LogTrace("Processing RabbitMQ event: {EventName}", eventName);

        if (_subsManager.HasSubscriptionsForEvent(eventName))
        {
            //执行已经订阅的方法
            await using var scope = _scopeFactory.CreateAsyncScope();
            var subscriptions = _subsManager.GetHandlersForEvent(eventName);
            foreach (var subscription in subscriptions)
            {
                if (subscription.IsDynamic)
                {
                    if (scope.ServiceProvider.GetRequiredService(subscription.HandlerType) is not IDynamicIntegrationEventHandler handler) continue;
                    using dynamic eventData = JsonDocument.Parse(message);
                    await Task.Yield();
                    await handler.Handle(eventData);
                }
                else
                {
                    var handler = scope.ServiceProvider.GetRequiredService(subscription.HandlerType);
                    if (handler == null) continue;
                    var jsonSerializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var eventType = _subsManager.GetEventTypeByName(eventName);
                    var integrationEvent = JsonSerializer.Deserialize(message, eventType, jsonSerializerOptions);
                    var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);

                    await Task.Yield();
                    await (Task)concreteType.GetMethod("Handle").Invoke(handler, [integrationEvent]);
                }
            }
        }
        else
        {
            _logger.LogWarning("No subscription for RabbitMQ event: {EventName}", eventName);
        }
    }
    /// <summary>
    /// 执行队列建立
    /// 与本地发布/订阅管理进行订阅事件检查，查看是否已存在订阅避免二次创建
    /// </summary>
    /// <param name="eventName"></param>
    private async Task DoInternalSubscriptionAsync(string eventName)
    {
        var containsKey = _subsManager.HasSubscriptionsForEvent(eventName);
        if (!containsKey)
        {
            CheckConnect();
            await _consumerChannel.QueueBindAsync(queue: _queueName,
                                 exchange: BROKER_NAME,
                                 routingKey: eventName);
        }
    }
    /// <summary>
    /// 从本地订阅管理中移除
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TH"></typeparam>
    public void Unsubscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
    {
        var eventName = _subsManager.GetEventKey<T>();

        _logger.LogInformation("Unsubscribing from event {EventName}", eventName);

        _subsManager.RemoveSubscription<T, TH>();
    }
    /// <summary>
    /// 从本地订阅管理中移除
    /// </summary>
    /// <typeparam name="TH"></typeparam>
    /// <param name="eventName"></param>
    public void UnsubscribeDynamic<TH>(string eventName) where TH : IDynamicIntegrationEventHandler
    {
        _subsManager.RemoveDynamicSubscription<TH>(eventName);
    }

    public void Dispose()
    {
        if (_consumerChannel != null)
        {
            _consumerChannel.Dispose();
        }

        _subsManager.Clear();
    }
}
