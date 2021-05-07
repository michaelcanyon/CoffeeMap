using System;
using System.Collections.Generic;

namespace CoffeeMapServer.Models
{
    public class Address : Entity
    {
        public Address() { }

        private Address(Guid? id = null)
            : base(id)
        { }

        public string AddressStr { get; set; }

        public string OpeningHours { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public ICollection<Roaster> Roasters { get; set; }

        public Guid RoasterId { get; set; }

        public static Address New(Guid id,
                                  string address,
                                  string openingHours,
                                  double latitude,
                                  double longitude)
            => new Address(id)
            {
                AddressStr = address,
                OpeningHours = openingHours,
                Latitude = latitude,
                Longitude = longitude
            };

        public static Address New(string address,
                                  string openingHours,
                                  double latitude,
                                  double longitude)
            => new Address
            {
                AddressStr = address,
                OpeningHours = openingHours,
                Latitude = latitude,
                Longitude = longitude
            };
    }
}