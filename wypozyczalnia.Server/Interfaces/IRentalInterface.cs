using wypozyczalnia.Server.DTOs;
using wypozyczalnia.Server.Models;

namespace wypozyczalnia.Server.Interfaces;

public interface IRentalInterface
{
    public Task StoreRental(MessageMgmConfirmed mess);
    
    //public Task RentToReturn(MessageMgmConfirmed mess);
    public Task<List<Rental>> GetPendingRentals();
    public Task<bool> AcceptRental(int rentalId);
    public Task<List<Rental>> GetRentalsToReturnAcceptance();
    
    //public Task<bool> AcceptReturnOfRental(int rentId);
    public Task<bool> AddPhotoToRental(int rentId, string photoUrl);
}