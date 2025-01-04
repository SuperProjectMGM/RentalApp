using wypozyczalnia.Server.DTOs;
using wypozyczalnia.Server.Models;

namespace wypozyczalnia.Server.Mappers;

public static class VehicleMapper
{
    public static VehicleDTO ToDTO(this Vehicle vehicle)
    {
        return new VehicleDTO
        {
            Brand = vehicle.Brand,
            Model = vehicle.Model,
            YearOfProduction = vehicle.YearOfProduction,
            Type = vehicle.Type,
            Price = vehicle.Price,
            DriveType = vehicle.DriveType,
            Transmission = vehicle.Transmission,
            Description = vehicle.Description,
            Rate = vehicle.Rate,
            Localization = vehicle.Localization,
            SerialNo = vehicle.SerialNo,
            Vin = vehicle.Vin,
            RegistryNo = vehicle.RegistryNo,
            PhotoUrl = vehicle.PhotoUrl
        };
    }


    public static Vehicle ToEntity(this VehicleDTO vehicleDTO)
    {
        return new Vehicle
        {
            Brand = vehicleDTO.Brand,
            Model = vehicleDTO.Model,
            YearOfProduction = vehicleDTO.YearOfProduction,
            Type = vehicleDTO.Type,
            Price = vehicleDTO.Price,
            DriveType = vehicleDTO.DriveType,
            Transmission = vehicleDTO.Transmission,
            Description = vehicleDTO.Description,
            Rate = vehicleDTO.Rate,
            Localization = vehicleDTO.Localization,
            SerialNo = vehicleDTO.SerialNo,
            Vin = vehicleDTO.Vin,
            RegistryNo = vehicleDTO.RegistryNo,
            PhotoUrl = vehicleDTO.PhotoUrl
        };
    }
}