using wypozyczalnia.Server.Models;
namespace wypozyczalnia.Server.DTOs;

public class RentalMessage
{
    public MessageType MessageType { get; set; }
        
    public string Slug { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public DateTime DrivingLicenseIssueDate { get; set; }
    public string PersonalNumber { get; set; } = string.Empty;
    public string LicenseNumber { get; set; } = string.Empty;    
    public string City { get; set; } = string.Empty;
    public string StreetAndNumber { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Vin { get; set; } = string.Empty;
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public RentalStatus Status { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; } 
    public float? ReturnLatitude { get; set; } = null;
    public float? ReturnLongtitude { get; set; } = null;
    public string? ReturnClientDescription { get; set; } = null;
}

public enum MessageType
{
    RentalMessageConfirmation = 0,
    RentalMessageCompletion = 1,
    RentalToReturn = 2,
    RentalAcceptedToReturn = 3
}
