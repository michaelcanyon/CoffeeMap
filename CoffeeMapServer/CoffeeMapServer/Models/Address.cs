using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Collections;
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

        public ICollection<Roaster> Roasters { get; set; }

        public Guid RoasterId { get; set; }

        public static Address New(Guid id, string address, string openingHours)
            => new Address(id)
            {
                AddressStr = address,
                OpeningHours = openingHours
            };

        public static Address New(string address, string openingHours)
            => new Address
            {
                AddressStr = address,
                OpeningHours = openingHours
            };
    }
}