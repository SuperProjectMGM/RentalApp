using System.Text;
using System.Text.Json;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using wypozyczalnia.Server.Interfaces;
using wypozyczalnia.Server.Messages;

namespace wypozyczalnia.Server.Services;

public class RabbitMessageService
{
    private readonly string _queueName = "browserToRental";

    private readonly string _queueName2 = "rentalToBrowser";

    private  IConnection? _connection;

    private IChannel? _channel;

    private IChannel? _channel2;

    private readonly IServiceProvider _serviceProvider;
    private readonly IConfiguration _configuration;

    public RabbitMessageService(IServiceProvider serviceProvider, IConfiguration configuration)
    {
        _serviceProvider = serviceProvider;
        _configuration = configuration;
    }
    
    public async Task Register()
    {
        try
        {
            var factory = new ConnectionFactory 
            {
                HostName = _configuration["RABBIT_HOST"]!,
                Port = 5672,
                UserName = _configuration["RABBIT_USERNAME"]!,
                Password = _configuration["RABBIT_PASSWORD"]!
            };
            
            _connection = await factory.CreateConnectionAsync();
            _channel = await _connection.CreateChannelAsync();
            _channel2 = await _connection.CreateChannelAsync();

            await _channel.QueueDeclareAsync(
                queue: _queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            await _channel2.QueueDeclareAsync(
                queue: _queueName2,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync += async (sender, ea) => await MessageReceived(sender, ea);
            await _channel.BasicConsumeAsync(_queueName, autoAck: true, consumer: consumer);
        }
        catch(Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during RabbitMQ registration: {ex.Message}");
            Console.Error.WriteLine(ex.StackTrace);   
        } 
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
            if (message is null)
                throw new Exception("Message corrupted.");
            using var scope = _serviceProvider.CreateScope();
            var rentalInterface = scope.ServiceProvider.GetRequiredService<IMessageHandlerInterface>();
            await rentalInterface.ProcessMessage(message);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing message: {ex.Message}");
        }
    }
    
    public async Task<bool> SendMessage(string message)
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