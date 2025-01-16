namespace wypozyczalnia.Server.Models;

public class ClientInfo
{
    public int ClientInfoId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public string LicenseNumber { get; set;} = string.Empty;
    public DateTime DrivingLicenseIssueDate { get; set; }
    public string PersonalNumber { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string StreetAndNumber { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
}