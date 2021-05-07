using System;
using CoffeeMapServer.Models;

namespace CoffeeMapServer.ViewModels.DTO
{
    public class TagDT : Entity
    {
        public string Name { get; set; }

        public TagDT()
        { }

        public TagDT(Guid? id = null) : base(id)
        { }

        public static TagDT New(Guid id, string name)
            => new TagDT(id)
            {
                Name = name
            };
    }
}
