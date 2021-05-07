using System;
using CoffeeMapServer.Models;

namespace CoffeeMapServer.ViewModels.DTO
{
    public class OwnerDT : Entity
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string PhoneNumber { get; set; }

        public OwnerDT()
        { }

        public OwnerDT(Guid? id = null) : base(id) { }

        public OwnerDT New(Guid id,
                           string name,
                           string surname,
                           string phoneNumber)
            => new OwnerDT(id)
            {
                Name = name,
                Surname = surname,
                PhoneNumber = phoneNumber
            };
    }
}
