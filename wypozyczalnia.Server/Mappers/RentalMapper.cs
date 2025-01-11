using System.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using wypozyczalnia.Server.DTOs;
using wypozyczalnia.Server.Models;
public static class RentalMapper
{
    public static RentalMessage ToRentalMessage(this Rental rental)
    {
        return new RentalMessage
        {
            Slug = rental.Slug,
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
            Description = rental.Description,
            Price = rental.Price,
            ReturnLatitude = rental.ReturnLatitude,
            ReturnLongtitude = rental.ReturnLongtitude,
            ReturnClientDescription = rental.ReturnClientDescription
        };
    }

    public static Rental ToRentalClientExists(this RentalMessage dto, ClientInfo userInfo)
    {
        return new Rental
        {
            Slug = dto.Slug,
            UserInfo = userInfo,
            Vin = dto.Vin,
            Start = dto.Start,
            End = dto.End,
            Status = dto.Status,
            Description = dto.Description,
            Price = dto.Price,
            ReturnLatitude = dto.ReturnLatitude,
            ReturnLongtitude = dto.ReturnLongtitude,
            ReturnClientDescription = dto.ReturnClientDescription        
        };
    }

    public static Rental ToRental(this RentalMessage dto)
    {
        // TODO: It's temporary object, it's important to check if such user is in our database or not.
        // Mati: Done, I believe.
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
            Slug = dto.Slug,
            UserInfo = userInfo,
            Vin = dto.Vin,
            Start = dto.Start,
            End = dto.End,
            Status = dto.Status,
            Description = dto.Description,        
            Price = dto.Price,
            ReturnLatitude = dto.ReturnLatitude,
            ReturnLongtitude = dto.ReturnLongtitude,
            ReturnClientDescription = dto.ReturnClientDescription      
        };
    }

    public static RentalDTO ToDto(this Rental rental)
    {
        return new RentalDTO
        {
            RentalId = rental.RentalId,
            Slug = rental.Slug,
            Name = rental.UserInfo.Name,
            Surname = rental.UserInfo.Surname,
            PersonalNumber = rental.UserInfo.PersonalNumber,
            Vin = rental.Vin,
            Start = rental.Start,
            End = rental.End,
            Status = rental.Status,
            Description = rental.Description,
            PhotoUrl = rental.PhotoUrl,
            ReturnLatitude = rental.ReturnLatitude,
            ReturnLongtitude = rental.ReturnLongtitude, 
            ReturnClientDescription = rental.ReturnClientDescription
        };
    }
}