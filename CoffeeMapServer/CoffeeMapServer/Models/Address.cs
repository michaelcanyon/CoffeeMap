using System;
using System.ComponentModel.DataAnnotations;

namespace CoffeeMapServer.Models
{
    public class Address : Entity
    {
        public Address() { }

        private Address(Guid? id = null)
            : base(id)
        { }

        [Required]
        public string AddressStr { get; set; }
        
        [Required]
        public string OpeningHours { get; set; }

        public static Address New(Guid id, string address, string openingHours)
            => new Address(id)
            {
                AddressStr = address,
                OpeningHours = openingHours
            };

        public static Address New(string address, string openingHours)
            => new Address()
            {
                AddressStr = address,
                OpeningHours = openingHours
            };
    }
}