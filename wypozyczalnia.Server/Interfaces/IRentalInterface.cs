using wypozyczalnia.Server.DTOs;
using wypozyczalnia.Server.Models;

namespace wypozyczalnia.Server.Interfaces;

public interface IRentalInterface
{
    public Task StoreRental(RentalMessage mess);
    public Task RentToReturn(RentalMessage mess);
    public Task<List<Rental>> GetPendingRentals();
    public Task<bool> AcceptRental(int rentalId);
}