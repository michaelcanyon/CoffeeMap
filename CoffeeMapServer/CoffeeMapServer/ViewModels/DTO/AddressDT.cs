using System;
using CoffeeMapServer.Models;

namespace CoffeeMapServer.ViewModels.DTO
{
    public class AddressDT : Entity
    {

        public string AddressStr { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string OpeningHours { get; set; }

        public AddressDT()
        { }

        public AddressDT(Guid? id = null) : base(id)
        { }

        public static AddressDT New(Guid id,
                                    string addressStr,
                                    string openingHours,
                                    double latitude,
                                    double longitude)
            => new AddressDT(id)
            {
                AddressStr = addressStr,
                OpeningHours = openingHours,
                Latitude=latitude,
                Longitude=longitude
            };
    }
}
