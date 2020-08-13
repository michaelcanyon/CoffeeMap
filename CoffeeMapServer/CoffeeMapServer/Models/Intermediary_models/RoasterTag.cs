using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeMapServer.Models.Intermediary_models
{
    public class RoasterTag:Entity
    {
        [Required]
        public int RoasterId { get; set; }
        [Required]
        public int TagId { get; set; }
    }

}
