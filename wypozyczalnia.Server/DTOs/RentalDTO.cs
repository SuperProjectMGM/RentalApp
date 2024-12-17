using wypozyczalnia.Server.Models;


namespace wypozyczalnia.Server.DTOs;
public class RentalDTO
{
    public string RentalId { get; set; } = string.Empty;
    // TODO: User class to handle all data about user
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    // TODO: Check ins econd api, I think here too can be DateOnly instead of string 
    public string BirthDate { get; set; } = string.Empty;
    public string LicenseNumber { get; set; } = string.Empty;
    public string DrivingLicenseIssueDate { get; set; } = string.Empty;
    public string PersonalNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string VinId { get; set; } = string.Empty;
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public RentalStatus Status { get; set; }
    public string Description { get; set; } = string.Empty;
}