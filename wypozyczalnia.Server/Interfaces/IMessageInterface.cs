using RabbitMQ.Client.Events;

namespace wypozyczalnia.Server.Interfaces;

public interface IMessageInterface
{
    public Task InitConsumer();

    public Task<string?> MessageReceived(object sender, BasicDeliverEventArgs eventArgs);
}