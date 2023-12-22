using RabbitMQ.Client;

namespace TakeFramework.EventBus.RabbitMQ;

public interface IPersistentConnection: IDisposable
{
   bool IsConnected { get; }

    bool TryConnect();

    IModel CreateModel();

}
