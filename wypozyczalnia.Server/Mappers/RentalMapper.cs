using wypozyczalnia.Server.DTOs;
using wypozyczalnia.Server.Models;
public static class RentalMapper
{
    public static RentalDTO ToRentalDto(this Rental rental)
    {
        return new RentalDTO
        {
            RentalId = rental.RentalId,
            Name = rental.UserInfo.Name,
            Surname = rental.UserInfo.Surname,
            BirthDate = rental.UserInfo.BirthDate.ToString(),
            LicenseNumber = rental.UserInfo.LicenseNumber,
            DrivingLicenseIssueDate = rental.UserInfo.DrivingLicenseIssueDate.ToString(),
            PersonalNumber = rental.UserInfo.PersonalNumber,
            Address = $"{rental.UserInfo.StreetName} {rental.UserInfo.HouseNumber}, {rental.UserInfo.PostalCode}",
            PhoneNumber = rental.UserInfo.PhoneNumber, 
            VinId = rental.Vin,
            Start = rental.Start,
            End = rental.End,
            Status = rental.Status,
            Description = rental.Description
        };
    }

    public static Rental ToRental(this RentalDTO dto)
    {
        string[] addrTmp = dto.Address.Split();
        // TODO: It's temporary object, it's important to check if such user is in our database
        // or not.
        ClientInfo userInfo = new ClientInfo
        {
            StreetName = addrTmp.Length >= 0 ? addrTmp[0] : "",
            HouseNumber = addrTmp.Length >= 1 ? addrTmp[1] : "",
            PostalCode = addrTmp.Length >= 2 ? addrTmp[2] : "",
            Name = dto.Name,
            Surname = dto.Surname,
            BirthDate = DateOnly.FromDateTime(DateTime.Parse(dto.BirthDate)),
            LicenseNumber = dto.LicenseNumber,
            DrivingLicenseIssueDate = DateOnly.FromDateTime(DateTime.Parse(dto.DrivingLicenseIssueDate)),
            PersonalNumber = dto.PersonalNumber,
            PhoneNumber = dto.PhoneNumber
        };

        return new Rental
        {
            UserInfo = userInfo,
            Vin = dto.VinId,
            Start = dto.Start,
            End = dto.End,
            Status = dto.Status,
            Description = dto.Description        
        };
    }
}