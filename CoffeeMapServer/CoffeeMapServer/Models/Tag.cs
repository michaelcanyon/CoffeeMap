using System;
using System.ComponentModel.DataAnnotations;
using System.Collections;
using System.Collections.Generic;

namespace CoffeeMapServer.Models
{
    public class Tag : Entity
    {
        public string TagTitle { get; set; }

        public ICollection<RoasterTag> RoasterTags { get; set; }

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