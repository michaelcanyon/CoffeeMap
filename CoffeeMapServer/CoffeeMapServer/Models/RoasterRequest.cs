using System;

namespace CoffeeMapServer.Models
{
    public class RoasterRequest : Entity
    {
        public Roaster Roaster { get; set; }

        public Address Address { get; set; }

        public RoasterRequest() { }

        public RoasterRequest(Guid? id = null) : base(id)
        { }

        public string TagString { get; set; }

        public static RoasterRequest New(Guid id,
                                         Roaster roaster,
                                         Address address,
                                         string tagString)
            => new RoasterRequest(id)
            {
                Roaster = roaster,
                Address = address,
                TagString=tagString
            };

        public static RoasterRequest New(Roaster roaster,
                                         Address address,
                                         string tagString)
            => new RoasterRequest
            {
                Roaster = roaster,
                Address = address,
                TagString = tagString
            };
    }
}