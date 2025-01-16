using System.Text.Json;
using wypozyczalnia.Server.BrowserProviders;
using wypozyczalnia.Server.DTOs;
using wypozyczalnia.Server.Interfaces;
using wypozyczalnia.Server.Mappers;
using wypozyczalnia.Server.Messages;

namespace wypozyczalnia.Server.Repositories;

public class MessageHandler : IMessageHandlerInterface
{

    private IRentalInterface _rentalRepo;

    public MessageHandler(IRentalInterface rentalRepo)
    {
        _rentalRepo = rentalRepo;
    }

    public async Task ProcessMessage(string serializedMess)
    {
        var msgWrap = JsonSerializer.Deserialize<MessageWrapper>(serializedMess);
        if (msgWrap is null)
            throw new Exception("Deserialized message corrupted.");
        switch (msgWrap.Type)
        {
            case MessageType.Confirmed:
                var confirmed = JsonSerializer.Deserialize<Confirmed>(msgWrap.Message);
                if (confirmed == null)
                    throw new Exception("Confirmation message corrupted.");
                await _rentalRepo.StoreRental(confirmed);
                break;
            case MessageType.UserReturn:
                var userReturn = JsonSerializer.Deserialize<UserReturn>(msgWrap.Message);
                if (userReturn == null)
                    throw new Exception("Return message corrupted.");
                await _rentalRepo.RentToReturn(userReturn);
                break;
            default:
                throw new KeyNotFoundException("Unknown message type.");
        }
    }

    public async Task ProcessExternalConfirm(ConfirmedExternal externalMessage)
    {
        var confirmed = externalMessage.ExternalToConfirmed();
        await _rentalRepo.StoreRental(confirmed);
    }

    public async Task ProcessExternalReturn(UserReturnExternal externalMessage)
    {
        var userReturn = externalMessage.ExternalToUserReturn();
        await _rentalRepo.RentToReturn(userReturn);
    }
    
}
