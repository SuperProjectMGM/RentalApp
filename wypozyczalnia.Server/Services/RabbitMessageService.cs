using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using wypozyczalnia.Server.Interfaces;
namespace wypozyczalnia.Server.Services;

public class RabbitMessageService
{
    private readonly string _queueName = "messageBox";

    private  IConnection? _connection;

    private IChannel? _channel;

    private readonly IServiceProvider _serviceProvider;

    public RabbitMessageService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public async Task Register()
    {
        var factory = new ConnectionFactory { HostName = "localhost" };

        _connection = await factory.CreateConnectionAsync();
        _channel = await _connection.CreateChannelAsync();

        await _channel.QueueDeclareAsync(
            queue: _queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.ReceivedAsync += async (sender, ea) => await MessageReceived(sender, ea);
        await _channel.BasicConsumeAsync(_queueName, autoAck: true, consumer: consumer);
    }

    private async Task MessageReceived(object sender, BasicDeliverEventArgs eventArgs)
    {
        try
        {
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            using var scope = _serviceProvider.CreateScope();
            var rentalService = scope.ServiceProvider.GetRequiredService<IRentalInterface>();
            await rentalService.StoreRental(message);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing message: {ex.Message}");
        }
    }

    public async Task Deregister()
    {
        await this._connection.CloseAsync();
    }
}