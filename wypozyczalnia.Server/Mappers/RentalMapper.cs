using System.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using wypozyczalnia.Server.DTOs;
using wypozyczalnia.Server.Models;
public static class RentalMapper
{
    public static Rental ToRentalClientExists(this Confirmed dto, ClientInfo userInfo)
    {
        return new Rental
        {
            BrowserProviderIdentifier = dto.BrowserProviderIdentifier,
            Slug = dto.Slug,
            UserInfo = userInfo,
            Vin = dto.Vin,
            Start = dto.Start,
            End = dto.End,
            Status = dto.Status,
            Description = dto.Description        
        };
    }

    public static Rental ToRental(this Confirmed dto)
    {
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
            BrowserProviderIdentifier = dto.BrowserProviderIdentifier,
            Slug = dto.Slug,
            UserInfo = userInfo,
            Vin = dto.Vin,
            Start = dto.Start,
            End = dto.End,
            Status = dto.Status,
            Description = dto.Description        
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
            PhotoUrl = rental.PhotoUrl
        };
    }
}