using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeMapServer.Models
{
    public class Tag:Entity
    {
        [Required]
        public string TagTitle { get; set; }
    }
}
