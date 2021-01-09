using System;
using System.ComponentModel.DataAnnotations;

namespace CoffeeMapServer.Models
{
    public class RoasterTag
    {
        public Guid RoasterId { get; set; }

        public Roaster Roaster { get; set; }

        public Guid TagId { get; set; }

        public Tag Tag { get; set; }

        public RoasterTag(Guid roasterId,
                          Guid tagId)
        {
            RoasterId = roasterId;
            TagId = tagId;
        }
    }
}