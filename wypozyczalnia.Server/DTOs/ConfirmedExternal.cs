namespace wypozyczalnia.Server.DTOs;

public class ConfirmedExternal
{
    public string BrowserProviderIdentifier { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public DateTime DrivingLicenseIssueDate { get; set; }
    public string PersonalNumber { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string StreetAndNumber { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Vin { get; set; } = string.Empty;
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public string ExternalOfferId { get; set; } = string.Empty;
}