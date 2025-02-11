using System.Data.SqlTypes;
using Microsoft.Data.SqlClient.Server;

namespace wypozyczalnia.Server.DTOs;
public class VehicleDTO
{
    public string Brand { get; set; } = string.Empty; 
    public string Model { get; set; } = string.Empty; 
    public int YearOfProduction { get; set; } 
    public string Type { get; set; } = string.Empty;
    public float Price { get; set; } 
    public string DriveType { get; set; } = string.Empty;
    public string Transmission { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty; 
    public int Rate { get; set; }
    public string Localization { get; set; } = string.Empty;
    public string SerialNo { get; set; } = string.Empty;
    public string Vin { get; set; } = string.Empty;
    public string RegistryNo { get; set; } = string.Empty; 
    public string PhotoUrl { get; set; } = string.Empty;
}