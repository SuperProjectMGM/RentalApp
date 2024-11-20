using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wypozyczalnia.Server.Models
{
    public class Vehicles
    {
        [Key]
        public string VehicleId { get; set; } = string.Empty;

        public string Brand { get; set; } = string.Empty;

        public string Model { get; set; } = string.Empty;

        public string SerialNo { get; set; } = string.Empty;

        public string VinId { get; set; } = string.Empty;

        public string RegistryNo { get; set; } = string.Empty;

        public int YearOfProduction { get; set; }

        public DateTime RentalFrom { get; set; }

        public DateTime RentalTo { get; set; }
        
        public decimal Prize { get; set; }

        public string DriveType { get; set; } = string.Empty;

        public string Transmission { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public decimal Rate { get; set; }

        public string Localization { get; set; } = string.Empty;

        public VehicleStatus VehicleStatus = VehicleStatus.Free;
    }
    
    public enum VehicleStatus
    {
        Free = 1,
        Rented = 2  
    }
}
