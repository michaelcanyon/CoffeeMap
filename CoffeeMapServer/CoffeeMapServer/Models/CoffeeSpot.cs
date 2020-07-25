using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeMapServer.Models
{
    public class CoffeeSpot:Entity
    {
        [Required]
        public string Title { get; set; }
        public object Picture { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int IndexId { get; set; }

    }
}
