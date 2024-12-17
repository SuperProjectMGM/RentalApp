using wypozyczalnia.Server.DTOs;
using wypozyczalnia.Server.Models;
public static class RentalMapper
{
    public static RentalMessage ToRentalMessage(this Rental rental)
    {
        return new RentalMessage
        {
            Name = rental.UserInfo.Name,
            Surname = rental.UserInfo.Surname,
            BirthDate = rental.UserInfo.BirthDate,
            LicenseNumber = rental.UserInfo.LicenseNumber,
            DrivingLicenseIssueDate = rental.UserInfo.DrivingLicenseIssueDate,
            PersonalNumber = rental.UserInfo.PersonalNumber,
            City = rental.UserInfo.City,
            StreetAndNumber = rental.UserInfo.StreetAndNumber,
            PostalCode = rental.UserInfo.PostalCode,
            PhoneNumber = rental.UserInfo.PhoneNumber, 
            Vin = rental.Vin,
            Start = rental.Start,
            End = rental.End,
            Status = rental.Status,
            Description = rental.Description
        };
    }

    public static Rental ToRental(this RentalMessage dto)
    {
        // TODO: It's temporary object, it's important to check if such user is in our database
        // or not.
        ClientInfo userInfo = new ClientInfo
        {
            City = dto.City,
            StreetAndNumber = dto.StreetAndNumber,
            PostalCode = dto.PostalCode,
            Name = dto.Name,
            Surname = dto.Surname,
            BirthDate = dto.BirthDate,
            LicenseNumber = dto.LicenseNumber,
            DrivingLicenseIssueDate = dto.DrivingLicenseIssueDate,
            PersonalNumber = dto.PersonalNumber,
            PhoneNumber = dto.PhoneNumber
        };

        return new Rental
        {
            UserInfo = userInfo,
            Vin = dto.Vin,
            Start = dto.Start,
            End = dto.End,
            Status = dto.Status,
            Description = dto.Description        
        };
    }
}