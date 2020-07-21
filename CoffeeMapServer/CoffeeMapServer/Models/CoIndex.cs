using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeMapServer.Models
{
    public class CoIndex:Entity
    {
        /// <summary>
        /// Espresso price
        /// </summary>
        [Required]
        public double Espresso { get; set; }

        /// <summary>
        /// Capuccino Price
        /// </summary>
        public double Capuccino { get; set; }
    }
}
