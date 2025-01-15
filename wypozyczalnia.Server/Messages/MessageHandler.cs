using System.Text.Json;
using wypozyczalnia.Server.DTOs;
using wypozyczalnia.Server.Interfaces;
using wypozyczalnia.Server.Messages;
namespace wypozyczalnia.Server.Repositories;

public class MessageHandler : IMessageHandlerInterface
{

    private IRentalInterface _rentalRepo;

    public MessageHandler(IRentalInterface rentalRepo)
    {
        _rentalRepo = rentalRepo;
    }

    public async Task ProcessMessage(MessageMgm decisionMsg, string serializedMsg)
    {
        switch (decisionMsg.MessageType)
        {
            case MessageType.UserConfirmedRental:
                var msg = JsonSerializer.Deserialize<MessageMgmConfirmed>(serializedMsg);
                if (msg is null)
                    throw new Exception("Deserialized confirmation message corrupted.");
                await _rentalRepo.StoreRental(msg);
                break;
            default:
                throw new KeyNotFoundException("Unknown message type.");
        }
    }
}