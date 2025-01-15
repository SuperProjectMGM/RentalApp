using wypozyczalnia.Server.Repositories;

namespace wypozyczalnia.Server.Models;

public class Rental
{
    public string BrowserProviderIdentifier { get; set; } = string.Empty; 
    public int RentalId { get; set; }
    public string Slug { get; set; } = string.Empty;
    public ClientInfo UserInfo { get; set; } = new ClientInfo();
    public string Vin { get; set; } = String.Empty;
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public RentalStatus Status { get; set; }
    public string Description { get; set; } = string.Empty;
    // If no employee is added than field is empty
    public string EmployeeId { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? PhotoUrl { get; set; } = null;
    public float? ReturnLatitude { get; set; } = null;
    public float? ReturnLongtitude { get; set; } = null;
    public string? ReturnEmployeeDescription { get; set; } = null;
    public string? ReturnClientDescription { get; set; } = null;
}

public enum RentalStatus
{
    Pending = 1,    
    Confirmed = 2,  
    Completed = 3,  
    WaitingForReturnAcceptance = 4,
    Returned = 5
}