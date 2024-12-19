using wypozyczalnia.Server.Models;
using wypozyczalnia.Server.DTOs;
namespace wypozyczalnia.Server.Interfaces;



public interface IVehicleInterface
{
    public Task<bool> CheckIfVehicleExists(string vin);
    public Task<Vehicle?> FindVehicle(string vin); 
    public Task<bool> DeleteVehicle(string vin);
    public Task<bool> AddVehicle(VehicleDTO vehicle);
    public Task<bool> ChangeVehicle(string vin, VehicleDTO vehicle);
    public Task<List<VehicleDTO>> ReturnVehicles();
    public Task<List<VehicleDTO>> ReturnVehicles(DateTime start, DateTime end);
}

