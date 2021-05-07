using System;

namespace CoffeeMapServer.Models
{
    public class Picture : Entity
    {
        public byte[] Bytes { get; set; }

        public Guid RoasterId { get; set; }

        public Roaster Roaster { get; set; }

        public Picture()
        { }

        public Picture(Guid? id = null) : base(id)
        { }

        public static Picture New(byte[] picture)
            => new Picture
            {
                Bytes = picture
            };

        public static Picture New(Guid id,
                                  byte[] picture)
            => new Picture
            {
                Id = id,
                Bytes = picture
            };
    }
}
