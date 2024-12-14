namespace wypozyczalnia.Server.Models;

public class UserInfo
{
    // ?? How does it work, when I create a new object?
    public int UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public DateOnly BirthDate { get; set; }
    public string LicenseNumber { get; set;} = string.Empty;
    public DateOnly DrivingLicenseIssueDate { get; set; }
    public string PersonalNumber { get; set; } = string.Empty;
    public Address Address { get; set; } = new Address();
    public string PhoneNumber { get; set; } = string.Empty;
    public IEnumerable<Rental> Rentals { get; set; } = new List<Rental>();
}