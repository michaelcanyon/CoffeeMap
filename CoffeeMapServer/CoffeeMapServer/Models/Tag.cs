using System;
using System.ComponentModel.DataAnnotations;

namespace CoffeeMapServer.Models
{
    public class Tag : Entity
    {
        [Required]
        public string TagTitle { get; set; }

        public Tag() { }

        public Tag(Guid? id = null) : base(id)
        { }

        public static Tag New(Guid id,
                              string tagTitle)
            => new Tag(id)
            {
                TagTitle = tagTitle
            };

        public static Tag New(string tagTitle)
            => new Tag
            {
                TagTitle = tagTitle
            };
    }
}