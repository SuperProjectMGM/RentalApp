using System.Text;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using NanoidDotNet;
using Newtonsoft.Json;
using NuGet.Protocol;
using wypozyczalnia.Server.DTOs;
using wypozyczalnia.Server.Interfaces;
using wypozyczalnia.Server.Models;
using wypozyczalnia.Server.Services;
using JsonSerializer = System.Text.Json.JsonSerializer;
using MessageType = Microsoft.DotNet.Scaffolding.Shared.Messaging.MessageType;

namespace wypozyczalnia.Server.Repositories;

public class RentalRepository : IRentalInterface
{
    private readonly RabbitMessageService _messageService;
    private readonly AppDbContext _context;
    public RentalRepository(AppDbContext context, RabbitMessageService messageService)
    {
        _messageService = messageService;
        _context = context;
    }
    
    // Mati: Michu znowu pisze enigmatyczne funkcje "na przyszłość"
    // public async Task CheckIfUserInfoInDatabase(Rental rental)
    // {
    //     ClientInfo info = rental.UserInfo;
    //     string personalNumber = info.PersonalNumber;
    //     //await _rentalsContext.
    // }
    
    public async Task StoreRental(RentalMessage mess)
    {
        var clientPersonalNumber = mess.PersonalNumber;
        var clientInfo = await _context.ClientInfos.FirstOrDefaultAsync(x => x.PersonalNumber == clientPersonalNumber);
        
        // TODO: So now we do not store slug, neither we add one. It is a matter to later consideration.
        if (clientInfo != null) // client is already in db
        {
           var rental = mess.ToRentalClientExists(clientInfo);
           await _context.Rentals.AddAsync(rental);
        }
        else
        {
            var rental = mess.ToRental();
            await _context.Rentals.AddAsync(rental);
        }

        await _context.SaveChangesAsync();
    }

    public async Task RentToReturn(RentalMessage mess)
    {
        // Looking for rental with Vin
        var rental = await _context.Rentals.FirstOrDefaultAsync(x => x.Vin == mess.Vin);
        if (rental == null)
            throw new Exception("Client not found in DB");
        // change status
        rental.Status = RentalStatus.WaitingForReturnAcceptance;
        await _context.SaveChangesAsync();
    }

    public async Task<List<Rental>> GetRentalsToReturnAcceptance()
    {
        List<Rental> ret = new List<Rental>();
        var tmp = await _context.Rentals.ToListAsync();
        foreach (var rent in tmp)
        {
            if (rent.Status == RentalStatus.WaitingForReturnAcceptance)
                ret.Add(rent);
        }

        return ret;
    }

    public async Task<List<Rental>> GetPendingRentals()
    {
        List<Rental> ret = new List<Rental>();
        var tmp = await _context.Rentals.ToListAsync();
        foreach (var rent in tmp)
        {
            if (rent.Status == RentalStatus.Confirmed)
                ret.Add(rent);
        }

        return ret;
    }

    public async Task<bool> AcceptRental(int rentalId)
    {
        // INFO: This might not work right now.
        var rental = await _context.Rentals.FirstOrDefaultAsync(x => x.RentalId == rentalId);
        if (rental is null)
            return false;
        rental.Status = RentalStatus.Completed;
        await _context.SaveChangesAsync();
        
        // send back to browser api
        var succeed = await SendCompletionMessage(rental);
        if (succeed)
            return true;
        return false;
    }

    public async Task<bool> SendCompletionMessage(Rental rental)
    {
        var rentalMess = rental.ToRentalMessage();
        rentalMess.MessageType = DTOs.MessageType.RentalMessageCompletion;
        string jsonString = JsonSerializer.Serialize(rentalMess);
        var succeed = await _messageService.SendMessage(jsonString);
        if (succeed)
            return true;
        return false;
    }
}