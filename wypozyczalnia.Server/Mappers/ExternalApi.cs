using NanoidDotNet;
using wypozyczalnia.Server.DTOs;
using wypozyczalnia.Server.Messages;
using wypozyczalnia.Server.Models;

namespace wypozyczalnia.Server.Mappers;

public static class ExternalApi
{
    public static Confirmed ExternalToConfirmed(this ConfirmedExternal dto)
    {
        return new Confirmed
        {
            // we have to set for external request our status
            Status = RentalStatus.Confirmed,
            BrowserProviderIdentifier = dto.BrowserProviderIdentifier,
            Slug = dto.ExternalOfferId + "_" + dto.BrowserProviderIdentifier,
            Name = dto.Name,
            Surname = dto.Surname,
            BirthDate = dto.BirthDate,
            DrivingLicenseIssueDate = dto.DrivingLicenseIssueDate,
            PersonalNumber = dto.PersonalNumber,
            City = dto.City,
            StreetAndNumber = dto.StreetAndNumber,
            PostalCode = dto.PostalCode,
            PhoneNumber = dto.PhoneNumber,
            Vin = dto.Vin,
            Start = dto.Start,
            End = dto.End
        };
    }

    public static UserReturn ExternalToUserReturn(this UserReturnExternal dto)
    {
        return new UserReturn
        {
            Slug = dto.ExternalOfferId
        };
    }
}
