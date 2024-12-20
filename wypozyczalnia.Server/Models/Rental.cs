using wypozyczalnia.Server.Repositories;

namespace wypozyczalnia.Server.Models;

public class Rental
{
    public string RentalId { get; set; } = string.Empty;
    
    public string Name { get; set; } = string.Empty;

    public string Surname { get; set; } = string.Empty;
    
    public string BirthDate { get; set; } = string.Empty;
    
    public string DateOfReceiptOfDrivingLicense { get; set; } = string.Empty;
    
    public string PersonalNumber { get; set; } = string.Empty;
    
    public string LicenceNumber { get; set; } = string.Empty;
    
    public string Address { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;
    
    public string VinId { get; set; } = string.Empty;
    
    public DateTime Start { get; set; }
    
    public DateTime End { get; set; }
    
    public RentalRepository.RentalStatus Status { get; set; }

    public string Description { get; set; } = string.Empty;
}