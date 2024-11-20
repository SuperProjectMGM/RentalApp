using NanoidDotNet;
using Newtonsoft.Json;
using wypozyczalnia.Server.Interfaces;
using wypozyczalnia.Server.Models;

namespace wypozyczalnia.Server.Repositories;

public class RentalRepository : IRentalInterface
{
    private readonly RentalsContext _rentalsContext;
    private readonly VehiclesContext _vehiclesContext;
    
    public RentalRepository(RentalsContext rentalsContext, VehiclesContext vehiclesContext)
    {
        _rentalsContext = rentalsContext;
        _vehiclesContext = vehiclesContext;
    }
    
    public async Task StoreRental(string message)
    {
        // rent data logic
        var rentalData = JsonConvert.DeserializeObject<Rental>(message);
        if (rentalData is null)
            throw new Exception("Wrong deserialized Rental obj");
        rentalData.RentalId = await Nanoid.GenerateAsync(Nanoid.Alphabets.LowercaseLettersAndDigits, 10);
        
        // vehicle logic
        
        await _rentalsContext.Rentals.AddAsync(rentalData);
        await _rentalsContext.SaveChangesAsync();
    }
    
    public enum RentalStatus
    {
        Pending = 1,    // Rental request is pending
        Confirmed = 2,  // Rental has been confirmed
        Completed = 3,  // Rental has been completed
    }
}