using wypozyczalnia.Server.Interfaces;

namespace wypozyczalnia.Server.Reposiotories;

public class RentalRepository : IRentalInterface
{
    private readonly IMessageInterface _messageService;

    public RentalRepository(IMessageInterface messageService)
    {
        _messageService = messageService;
        _messageService.InitConsumer();
    }
    
    public enum RentalStatus
    {
        Pending = 1,    // Rental request is pending
        Confirmed = 2,  // Rental has been confirmed
        Completed = 3,  // Rental has been completed
    }

    public Task StoreNewRental()
    {
        throw new NotImplementedException();
    }
}