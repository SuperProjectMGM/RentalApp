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
        var msg = new MessageMgmCompleted
        {
            MessageType = MessageType.EmployeeCompletedRental,
            Slug = rental.Slug
        };
        var jsonStr = JsonSerializer.Serialize(msg);
        await _messageService.SendMessage(jsonStr);
    }
}
