using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeMapServer.Models.OwnedModels
{
    public class OwnedAddress
    {
        public string AddressStr { get; set; }

        public string OpeningHours { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public OwnedAddress()
        { }

        public static OwnedAddress New(string addressStr,
                                    string openingHours,
                                    double latitude,
                                    double longitude)
            => new OwnedAddress()
            {
                AddressStr = addressStr,
                OpeningHours = openingHours,
                Latitude = latitude,
                Longitude = longitude
            };

    }
}
