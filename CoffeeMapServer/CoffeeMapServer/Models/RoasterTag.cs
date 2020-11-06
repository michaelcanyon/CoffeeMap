using System;
using System.ComponentModel.DataAnnotations;

namespace CoffeeMapServer.Models.Intermediary_models
{
    public class RoasterTag
    {
        [Required]
        public Guid RoasterId { get; set; }

        //[Required]
        //public Roaster Roaster { get; set; }

        [Required]
        public Guid TagId { get; set; }

        //[Required]
        //public Tag Tag { get; set; }

        public RoasterTag(
            Guid roasterId,
            Guid tagId)
        {
            RoasterId = roasterId;
            TagId = tagId;
        }
    }
}
