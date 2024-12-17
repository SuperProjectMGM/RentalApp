namespace wypozyczalnia.Server.Models;

public class ClientInfo
{
    // ?? How does it work, when I create a new object?
    public int UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public DateOnly BirthDate { get; set; }
    public string LicenseNumber { get; set;} = string.Empty;
    public DateOnly DrivingLicenseIssueDate { get; set; }
    public string PersonalNumber { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string StreetAndNumber { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
}