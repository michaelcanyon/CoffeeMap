using System.ComponentModel.DataAnnotations;

namespace CoffeeMapServer.Models
{
    public class Address : Entity
    {
        [Required]
        public string AddressStr { get; set; }
        [Required]
        public string OpeningHours { get; set; }
    }
}
