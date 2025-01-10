using wypozyczalnia.Server.Repositories;

namespace wypozyczalnia.Server.Models;

public class Rental
{
    public int RentalId { get; set; }
    public string Slug { get; set; } = string.Empty;
    public ClientInfo UserInfo { get; set; } = new ClientInfo();
    public string Vin { get; set; } = String.Empty;
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public RentalStatus Status { get; set; }
    public string Description { get; set; } = string.Empty;
    public Employee? Employee { get; set; } = null;
    public string? PhotoUrl { get; set; } = null;
}

public enum RentalStatus
{
    // TODO: Check if this status is necessary, I think that it is never used
    Pending = 1,    
    Confirmed = 2,  
    Completed = 3,  
    WaitingForReturnAcceptance = 4,
    Returned = 5
}