using wypozyczalnia.Server.Repositories;

namespace wypozyczalnia.Server.Models;

public class Rental
{
    public string RentalId { get; set; } = string.Empty;
    // TODO: User class to handle all data about user
    //public string Name { get; set; } = string.Empty;
//
    //public string Surname { get; set; } = string.Empty;
    //// TODO: change to datetime
    //public string BirthDate { get; set; } = string.Empty;
    //// TODO: Change to datetime
    //public string DateOfReceiptOfDrivingLicense { get; set; } = string.Empty;
    //
    //public string PersonalNumber { get; set; } = string.Empty;
    //
    //public string LicenceNumber { get; set; } = string.Empty;
    //
    //public string Address { get; set; } = string.Empty;
//
    //public string PhoneNumber { get; set; } = string.Empty;
    //
    //public string VinId { get; set; } = string.Empty;
    
    public UserInfo UserInfo { get; set; }

    public DateTime Start { get; set; }
    
    public DateTime End { get; set; }
    public RentalStatus Status { get; set; }

    public string Description { get; set; } = string.Empty;
}

public enum RentalStatus
{
    Pending = 1,    // Rental request is pending
    Confirmed = 2,  // Rental has been confirmed
    Completed = 3,  // Rental has been completed
}