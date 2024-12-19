using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using wypozyczalnia.Server.Interfaces;
namespace wypozyczalnia.Server.Services;

public class RabbitMessageService
{
    private readonly string _queueName = "messageBox";

    private readonly string _queueName2 = "messageBox2";

    private  IConnection? _connection;

    private IChannel? _channel;

    private IChannel? _channel2;

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
        _channel2 = await _connection.CreateChannelAsync();

        await _channel.QueueDeclareAsync(
            queue: _queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
        
        await _channel2.QueueDeclareAsync(
            queue: _queueName2,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.ReceivedAsync += async (sender, ea) => await MessageReceived(sender, ea);
        await _channel.BasicConsumeAsync(_queueName, autoAck: true, consumer: consumer);
    }
    public async Task Deregister()
    {
        await _connection.CloseAsync();
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
    
    public async Task<bool> SendRentalCompletion(string message)
    {
        var encodedMes = Encoding.UTF8.GetBytes(message);
        var memoryBody = new ReadOnlyMemory<byte>(encodedMes);
        try
        {
            await _channel2.BasicPublishAsync(
                exchange: "",
                routingKey: _queueName2,
                body: memoryBody
            );
            
            return true;
        }
        catch
        {
            return false;
        }
    }

}