using System;
using System.ComponentModel.DataAnnotations;

namespace CoffeeMapServer.Models.Intermediary_models
{
    public class RoasterTag : Entity
    {
        [Required]
        public Guid RoasterId { get; set; }
        [Required]
        public Guid TagId { get; set; }
    }

}
