using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wypozyczalnia.Server.Models
{
    public class Vehicles
    {
        [Key]
        public int CarId { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Brand { get; set; } = "";

        [Column(TypeName = "nvarchar(100)")]
        public string Model { get; set; } = "";

        [Column(TypeName = "nvarchar(100)")]
        public string SerialNo { get; set; } = "";

        [Column(TypeName = "nvarchar(100)")]
        public string VinId { get; set; } = "";

        [Column(TypeName = "nvarchar(50)")]
        public string RegistryNo { get; set; } = "";

        [Column(TypeName = "int")]
        public int YearOfProduction { get; set; }

        [Column(TypeName = "date")]
        public DateTime RentalFrom { get; set; }

        [Column(TypeName = "date")]
        public DateTime RentalTo { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Prize { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        public string DriveType { get; set; } = "";

        [Column(TypeName = "nvarchar(10)")]
        public string Transmission { get; set; } = "";

        [Column(TypeName = "nvarchar(500)")]
        public string Description { get; set; } = "";

        [Column(TypeName = "nvarchar(50)")]
        public string Type { get; set; } = "";

        [Column(TypeName = "decimal(18,2)")]
        public decimal Rate { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Localization { get; set; } = "";
    }
}
