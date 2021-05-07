using System;
using CoffeeMapServer.Models.OwnedModels;

namespace CoffeeMapServer.Models
{
    public class PictureRequest : Entity
    {
        public byte[] Bytes { get; set; }

        public Guid RoasterRequestId { get; set; }

        public RoasterRequest RoasterRequest{ get; set; }

        public PictureRequest()
        { }

        public PictureRequest(Guid? id = null) : base(id)
        { }

        public static PictureRequest New(byte[] picture)
            => new PictureRequest
            {
                Bytes = picture
            };

        public static PictureRequest New(Guid id,
                                         byte[] picture)
            => new PictureRequest
            {
                Id = id,
                Bytes = picture
            };
    }
}
