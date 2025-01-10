using wypozyczalnia.Server.Models;

namespace wypozyczalnia.Server.DTOs;

public class RentalDTO
{        
    public int RentalId { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string PersonalNumber { get; set; } = string.Empty;
    public string Vin { get; set; } = string.Empty;
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public RentalStatus Status { get; set; }
    public string Description { get; set; } = string.Empty;
    public string? PhotoUrl { get; set; } = null; 
    
}