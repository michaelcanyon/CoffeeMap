using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeMapServer.Models
{
    public class Address:Entity
    {
        [Required]
        public string AddressStr { get; set; }
        [Required]
        public string OpeningHours { get; set; }
    }
}
