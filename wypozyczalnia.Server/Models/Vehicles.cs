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

    }

}
