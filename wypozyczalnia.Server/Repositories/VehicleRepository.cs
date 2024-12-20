using Microsoft.EntityFrameworkCore;
using wypozyczalnia.Server.DTOs;
using wypozyczalnia.Server.Interfaces;
using wypozyczalnia.Server.Mappers;
using wypozyczalnia.Server.Models;

public class VehicleRepository : IVehicleInterface
{
    private readonly AppDbContext _appDbContext;
    public VehicleRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<bool> AddVehicle(VehicleDTO vehicleDTO)
    { 
        try
        {
            Vehicle vehicle = vehicleDTO.ToEntity();
            _appDbContext.Add(vehicle);
            await _appDbContext.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false; 
        }
    }

    public async Task<bool> ChangeVehicle(string vin, VehicleDTO vehicle)
    {
        if (!await CheckIfVehicleExists(vin))
            return false;

        // I've checked if vehicle exists before, now can Find
        Vehicle? veh = await FindVehicle(vin);

        try
        {
            // Why it works only this way, even method with ref doesn't apply chagnes
            veh.Brand = vehicle.Brand;
            veh.Model = vehicle.Model;
            veh.YearOfProduction = vehicle.YearOfProduction;
            veh.Type = vehicle.Type;
            veh.Price = vehicle.Price;
            veh.DriveType = vehicle.DriveType;
            veh.Transmission = vehicle.Transmission;
            veh.Description = vehicle.Description;
            veh.Rate = vehicle.Rate;
            veh.Localization = vehicle.Localization;
            veh.SerialNo = vehicle.SerialNo;
            veh.RegistryNo = vehicle.RegistryNo;
            await _appDbContext.SaveChangesAsync();
            return true;
        }
        catch
        {
            // Here must be log
            return false;
        }
    }

    public async Task<bool> CheckIfVehicleExists(string vin)
    {
        return await _appDbContext.Vehicles.AnyAsync(vehicle => vehicle.Vin == vin);
    }

    public async Task<bool> DeleteVehicle(string vin)
    {
        if (!await CheckIfVehicleExists(vin))
            return false;

        Vehicle? veh = await FindVehicle(vin);

        try 
        {
            _appDbContext.Vehicles.Remove(veh!);
            await _appDbContext.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<Vehicle?> FindVehicle(string vin)
    {
        return await _appDbContext.Vehicles.SingleOrDefaultAsync(veh => veh.Vin == vin);
    }

    public async Task<List<VehicleDTO>> ReturnVehicles()
    {
        var list = await _appDbContext.Vehicles.ToListAsync();
        var dtoList = list.Select(veh => veh.ToDTO());
        return dtoList.ToList();
    }

    public async Task<List<VehicleDTO>> ReturnVehicles(DateTime start, DateTime end)
    {
        var vehicles = await _appDbContext.Vehicles.ToListAsync();
        var availableVehicles = vehicles
        .Where(vehicle => !_appDbContext.Rentals.Any(rental => vehicle.Vin == rental.Vin 
        && rental.Start <= end && rental.End >= start && rental.Status != RentalStatus.Returned))
        .Select(veh => veh.ToDTO()).ToList();
        return availableVehicles;
    }
}