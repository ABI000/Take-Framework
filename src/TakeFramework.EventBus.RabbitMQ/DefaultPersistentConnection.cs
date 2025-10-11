using RabbitMQ.Client;
using Polly.Retry;
using System.Net.Sockets;
using RabbitMQ.Client.Exceptions;
using Polly;
using RabbitMQ.Client.Events;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
namespace TakeFramework.EventBus.RabbitMQ;
///
public class DefaultPersistentConnection : IPersistentConnection
{
    private readonly PersistentConnectionOptions _options;
    private readonly IConnectionFactory _connectionFactory;
    private readonly ILogger<DefaultPersistentConnection> _logger;
    private readonly int _retryCount;
    public DefaultPersistentConnection(ILogger<DefaultPersistentConnection> logger, IOptions<PersistentConnectionOptions> options)
    {
        _options = options?.Value ?? throw new ArgumentNullException(nameof(logger));
        _connectionFactory = new ConnectionFactory { HostName = _options.HostName, UserName = _options.UserName, Password = _options.Password };
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _retryCount = options.Value.RetryCount;
    }
    private IConnection? _connection;
    public bool Disposed;

    readonly object _syncRoot = new();

    public bool IsConnected => _connection is { IsOpen: true } && !Disposed;

    public async Task<IChannel> CreateChannelAsync()
    {
        if (!IsConnected)
        {
            throw new InvalidOperationException("No RabbitMQ connections are available to perform this action");
        }

        return await _connection!.CreateChannelAsync();
    }

    public void Dispose()
    {
        if (Disposed) return;

        Disposed = true;

        try
        {
            _connection!.ConnectionShutdownAsync -= OnConnectionShutdownAsync;
            _connection.CallbackExceptionAsync -= OnCallbackExceptionAsync;
            _connection.ConnectionBlockedAsync -= OnConnectionBlockedAsync;
            _connection.Dispose();
        }
        catch (IOException ex)
        {
            _logger.LogCritical(ex, "RabbitMQ critical exception: {ExceptionMessage}", ex.Message);
        }
        GC.SuppressFinalize(this);
    }

    public bool TryConnect()
    {
        _logger.LogInformation("RabbitMQ Client is trying to connect");

        lock (_syncRoot)
        {
            RetryPolicy policy = Policy.Handle<SocketException>()
                .Or<BrokerUnreachableException>()
                .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                {
                    _logger.LogWarning(ex, "RabbitMQ Client could not connect after {TimeOut}s ({ExceptionMessage})", $"{time.TotalSeconds:n1}", ex.Message);
                }
            );

            policy.Execute(async () =>
            {
                _connection = await _connectionFactory.CreateConnectionAsync();
            });

            if (IsConnected)
            {
                _connection!.ConnectionShutdownAsync += OnConnectionShutdownAsync;
                _connection.CallbackExceptionAsync += OnCallbackExceptionAsync;
                _connection.ConnectionBlockedAsync += OnConnectionBlockedAsync;

                _logger.LogInformation("RabbitMQ Client acquired a persistent connection to '{HostName}' and is subscribed to failure events", _connection.Endpoint.HostName);

                return true;
            }
            else
            {
                _logger.LogCritical("FATAL ERROR: RabbitMQ connections could not be created and opened");

                return false;
            }
        }
    }

    // 修改事件处理方法为 async Task 返回类型以匹配 AsyncEventHandler 委托签名
    private async Task OnConnectionBlockedAsync(object sender, ConnectionBlockedEventArgs e)
    {
        if (Disposed) return;

        _logger.LogWarning("A RabbitMQ connection is shutdown. Trying to re-connect...");

        TryConnect();
        await Task.CompletedTask;
    }

    private async Task OnCallbackExceptionAsync(object sender, CallbackExceptionEventArgs e)
    {
        if (Disposed) return;

        _logger.LogWarning("A RabbitMQ connection throw exception. Trying to re-connect...");

        TryConnect();
        await Task.CompletedTask;
    }

    private async Task OnConnectionShutdownAsync(object sender, ShutdownEventArgs reason)
    {
        if (Disposed) return;

        _logger.LogWarning("A RabbitMQ connection is on shutdown. Trying to re-connect...");

        TryConnect();
        await Task.CompletedTask;
    }
}
