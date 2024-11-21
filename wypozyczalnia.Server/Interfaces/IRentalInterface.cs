using wypozyczalnia.Server.Models;

namespace wypozyczalnia.Server.Interfaces;

public interface IRentalInterface
{
    public Task StoreRental(string message);

    public Task<List<Rental>> GetPendingRentals();

    public Task<bool> AcceptRental(string rentalId);

    public Task<bool> SendCompletionMessage(Rental rental);
}