using System.Text.Json;
using wypozyczalnia.Server.Messages;
using wypozyczalnia.Server.Models;
using wypozyczalnia.Server.Services;

namespace wypozyczalnia.Server.BrowserProviders;

public class MgmBrowser
{
    private readonly RabbitMessageService _messageService;

    public MgmBrowser(RabbitMessageService rabbit)
    {
        _messageService = rabbit;
    }

    public async Task RentalCompleted(Rental rental)
    {
        var msg = new Completed
        {
            Slug = rental.Slug
        };
        var jsonStr = JsonSerializer.Serialize(msg);
        var msgWrap = new MessageWrapper
        {
            Message = jsonStr,
            Type = MessageType.Completed
        };
        var jsonWrap = JsonSerializer.Serialize(msgWrap);
        await _messageService.SendMessage(jsonWrap);
    }

    public async Task AcceptReturn(Rental rental, Vehicle vehicle)
    {
        // Calculate due, every started day counts 
        int days = rental.End.Day - rental.Start.Day + 1;
        float due = vehicle.Price * days;
        
        var msg = new EmployeeReturn
        {
            PaymentDue = due,
            Slug = rental.Slug
        };
        var jsonStr = JsonSerializer.Serialize(msg);
        var msgWrap = new MessageWrapper
        {
            Message = jsonStr,
            Type = MessageType.EmployeeReturn,
        };
        var jsonWrap = JsonSerializer.Serialize(msgWrap);
        await _messageService.SendMessage(jsonWrap);
    }
}
