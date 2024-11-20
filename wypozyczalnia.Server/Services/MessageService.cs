using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using wypozyczalnia.Server.Interfaces;

namespace wypozyczalnia.Server.Services;

public class MessageService : IMessageInterface
{
    private readonly string _queueName = "messageBox";

    private AsyncEventingBasicConsumer ConsumerForSearch;
    
    public event EventHandler<string?> MessageDelivered;
    
    public async Task InitConsumer()
    {
        var factory = new ConnectionFactory { HostName = "localhost" };

        await using var connection = await factory.CreateConnectionAsync();
        await using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(
            queue: _queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        ConsumerForSearch = new AsyncEventingBasicConsumer(channel);
        ConsumerForSearch.ReceivedAsync += async (sender, ea) => await MessageReceived(sender, ea);
    }

    public async Task<string?> MessageReceived(object sender, BasicDeliverEventArgs eventArgs)
    {
        try
        {
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            return await Task.FromResult<string?>(message);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing message: {ex.Message}");
            return await Task.FromResult<string?>(null);
        }
    }
}