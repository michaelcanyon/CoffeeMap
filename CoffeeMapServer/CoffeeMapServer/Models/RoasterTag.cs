using System;
using System.ComponentModel.DataAnnotations;

namespace CoffeeMapServer.Models
{
    public class RoasterTag
    {
        [Required]
        public Guid RoasterId { get; set; }

        [Required]
        public Guid TagId { get; set; }

        public RoasterTag(Guid roasterId,
                          Guid tagId)
        {
            RoasterId = roasterId;
            TagId = tagId;
        }
    }
}