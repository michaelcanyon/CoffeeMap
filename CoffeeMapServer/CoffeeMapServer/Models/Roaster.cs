using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeMapServer.Models
{
    public class Roaster:Entity
    {
        /// <summary>
        /// Roaster Name
        /// </summary>
        [Required]
        public string Name { get; set; }
    }
}
