using System;
using CoffeeMapServer.Models.OwnedModels;

namespace CoffeeMapServer.Models
{
    public class RoasterRequest : Entity
    {
        public OwnedRoaster Roaster { get; set; }

        public OwnedAddress Address { get; set; }

        public string Tags { get; set; }

        public Guid PictureRequestId { get; set; }

        public PictureRequest Picture { get; set; }

        public RoasterRequest() { }

        public RoasterRequest(Guid? id = null) : base(id)
        { }

        public string TagString { get; set; }

        public static RoasterRequest New(Guid id,
                                         OwnedRoaster roaster,
                                         OwnedAddress address,
                                         string tagString)
            => new RoasterRequest(id)
            {
                Roaster = roaster,
                Address = address,
                TagString = tagString
            };

        public static RoasterRequest New(OwnedRoaster roaster,
                                         OwnedAddress address,
                                         string tagString)
            => new RoasterRequest
            {
                Roaster = roaster,
                Address = address,
                TagString = tagString
            };
    }
}