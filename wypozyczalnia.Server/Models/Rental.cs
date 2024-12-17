using wypozyczalnia.Server.Repositories;

namespace wypozyczalnia.Server.Models;

public class Rental
{
    public string RentalId { get; set; } = string.Empty;
    public UserInfo UserInfo { get; set; } = new UserInfo();

    public string Vin { get; set; } = String.Empty;
    public DateTime Start { get; set; }
    
    public DateTime End { get; set; }
    public RentalStatus Status { get; set; }

    public string Description { get; set; } = string.Empty;
}

public enum RentalStatus
{
    Pending = 1,    // Rental request is pending
    Confirmed = 2,  // Rental has been confirmed
    Completed = 3,  // Rental has been completed
}