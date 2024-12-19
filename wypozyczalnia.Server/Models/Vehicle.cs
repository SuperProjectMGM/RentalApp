using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Data.SqlTypes;

namespace wypozyczalnia.Server.Models
{
    // TODO: rename to vehicle
    // Mati: whatt??
    public class Vehicle
    {
        public int VehicleId { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string SerialNo { get; set; } = string.Empty;
        public string Vin { get; set; } = string.Empty;
        public string RegistryNo { get; set; } = string.Empty;
        public int YearOfProduction { get; set; }
        
        [Column(TypeName = "money")]
        public float Price { get; set; }
        public string DriveType { get; set; } = string.Empty;
        public string Transmission { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        
        // TODO: Think what should be here
        public int Rate { get; set; }
        public string Localization { get; set; } = string.Empty;
        public string PhotoUrl { get; set; } = string.Empty;
    }
}
