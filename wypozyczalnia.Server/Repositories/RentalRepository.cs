using System.Text;
using Microsoft.EntityFrameworkCore;
using NanoidDotNet;
using Newtonsoft.Json;
using wypozyczalnia.Server.Interfaces;
using wypozyczalnia.Server.Models;
using wypozyczalnia.Server.Services;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace wypozyczalnia.Server.Repositories;

public class RentalRepository : IRentalInterface
{
    private readonly RentalsContext _rentalsContext;
    private readonly RabbitMessageService _messageService;
    
    public RentalRepository(RentalsContext rentalsContext, RabbitMessageService messageService)
    {
        _rentalsContext = rentalsContext;
        _messageService = messageService;
    }
    
    public async Task StoreRental(string message)
    {
        // rent data logic
        var rentalData = JsonConvert.DeserializeObject<Rental>(message);
        if (rentalData is null)
            throw new Exception("Wrong deserialized Rental obj");
        rentalData.RentalId = await Nanoid.GenerateAsync(Nanoid.Alphabets.LowercaseLettersAndDigits, 10);
        
        // vehicle logic ??
        
        await _rentalsContext.Rentals.AddAsync(rentalData);
        await _rentalsContext.SaveChangesAsync();
    }

    public async Task<List<Rental>> GetPendingRentals()
    {
        List<Rental> ret = new List<Rental>();
        var tmp = await _rentalsContext.Rentals.ToListAsync();
        foreach (var rent in tmp)
        {
            if (rent.Status == RentalStatus.Confirmed)
                ret.Add(rent);
        }

        return ret;
    }

    public async Task<bool> AcceptRental(string rentalId)
    {
        var rental = await _rentalsContext.Rentals.FirstOrDefaultAsync(x => x.RentalId == rentalId);
        if (rental is null)
            return false;
        rental.Status = RentalStatus.Completed;
        await _rentalsContext.SaveChangesAsync();
        
        // send back to browser api
        var succeed = await SendCompletionMessage(rental);
        if (succeed)
            return true;
        else
            return false;
    }

    public async Task<bool> SendCompletionMessage(Rental rental)
    {
        string jsonString = JsonSerializer.Serialize(rental);
        var succeed = await _messageService.SendRentalCompletion(jsonString);
        if (succeed)
            return true;
        else
            return false;
    }

    public enum RentalStatus
    {
        Pending = 1,    // Rental request is pending
        Confirmed = 2,  // Rental has been confirmed
        Completed = 3,  // Rental has been completed
    }
    

}