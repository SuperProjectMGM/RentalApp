namespace wypozyczalnia.Server.Reposiotories;

public class RentalRepository
{
    
    
    public enum RentalStatus
    {
        Pending = 1,    // Rental request is pending
        Confirmed = 2,  // Rental has been confirmed
        Completed = 3,  // Rental has been completed
    }
}