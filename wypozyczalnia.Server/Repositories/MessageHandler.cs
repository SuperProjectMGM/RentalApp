using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using wypozyczalnia.Server.DTOs;
using wypozyczalnia.Server.Interfaces;
using wypozyczalnia.Server.Models;

namespace wypozyczalnia.Server.Repositories;

public class MessageHandler : IMessageHandler
{

    private IRentalInterface _rentalRepo;
    
    public MessageHandler(IRentalInterface rentalRepo)
    {
        _rentalRepo = rentalRepo;
    }
    
    public async Task ProcessMessage(string message)
    {
        var mess = JsonConvert.DeserializeObject<RentalMessage>(message);
        if (mess == null)
            throw new Exception("Error during processing message.");
        switch (mess.MessageType)
        {
            case MessageType.RentalMessageConfirmation:
                await _rentalRepo.StoreRental(mess);
                break;
            case MessageType.RentalToReturn:
                await _rentalRepo.RentToReturn(mess);
                break;
        }
    }
}