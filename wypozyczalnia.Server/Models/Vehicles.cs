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
        [Key]
        public string VinId { get; set; } = "";

        [Column(TypeName = "nvarchar(50)")]
        public string RegistryNo { get; set; } = "";

        [Column(TypeName = "nvarchar(4)")]
        public string YearOfProduction { get; set; } = "";

        [Column(TypeName = "nvarchar(10)")] // dd/mm/yyyy
        public string RentalFrom { get; set; } = "";

        [Column(TypeName = "nvarchar(10)")]
        public string RentalTo { get; set; } = "";

        [Column(TypeName = "nvarchar(500)")]
        public string Description { get; set; } = "";

        [Column(TypeName = "nvarchar(50)")]
        public string Type { get; set; } = "";
    }
}
