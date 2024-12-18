using Microsoft.EntityFrameworkCore;
using wypozyczalnia.Server.DTOs;
using wypozyczalnia.Server.Interfaces;
using wypozyczalnia.Server.Mappers;
using wypozyczalnia.Server.Models;

public class VehicleRepository : IVehicleInterface
{
    private readonly RentalsContext _rentalsContext;
    private readonly VehiclesContext _vehiclesContext;
    public VehicleRepository(RentalsContext rentalContext, VehiclesContext vehicleContext)
    {
        _rentalsContext = rentalContext;
        _vehiclesContext = vehicleContext;
    }
    public async Task<bool> AddVehicle(VehicleDTO vehicleDTO)
    { 
        try
        {
            Vehicle vehicle = vehicleDTO.ToEntity();
            _vehiclesContext.Add(vehicle);
            await _vehiclesContext.SaveChangesAsync();
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
            await _vehiclesContext.SaveChangesAsync();
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
        return await _vehiclesContext.Vehicles.AnyAsync(vehicle => vehicle.Vin == vin);
    }

    public async Task<bool> DeleteVehicle(string vin)
    {
        if (!await CheckIfVehicleExists(vin))
            return false;

        Vehicle? veh = await FindVehicle(vin);

        try 
        {
            _vehiclesContext.Vehicles.Remove(veh!);
            await _vehiclesContext.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<Vehicle?> FindVehicle(string vin)
    {
        return await _vehiclesContext.Vehicles.SingleOrDefaultAsync(veh => veh.Vin == vin);
    }

    public async Task<List<VehicleDTO>> ReturnVehicles()
    {
        var list = await _vehiclesContext.Vehicles.ToListAsync();
        var dtoList = list.Select(veh => veh.ToDTO());
        return dtoList.ToList();
    }

    public async Task<List<VehicleDTO>> ReturnVehicles(DateTime start, DateTime end)
    {
        var vehicles = await _vehiclesContext.Vehicles.ToListAsync();
        var availableVehicles = vehicles
        .Where(vehicle => !_rentalsContext.Rentals.Any(rental => vehicle.Vin == rental.Vin 
        && rental.Start <= end && rental.End >= start)).Select(veh => veh.ToDTO()).ToList();
        return availableVehicles;
    }
}