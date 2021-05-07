using System;
using CoffeeMapServer.Models;
using CoffeeMapServer.Models.OwnedModels;

namespace CoffeeMapServer.builders
{
    public static class AddressCoordinatesTransformer
    {
        public static Address ConvertCoordinates(Address address, string latitude, string longitude)
        {
            try
            {
                if (latitude != null &&
                    longitude != null)
                {
                    address.Latitude = Convert.ToDouble(latitude.Replace('.', ','));
                    address.Longitude = Convert.ToDouble(longitude.Replace('.', ','));
                }
            }
            catch
            {
                address.Latitude = 0;
                address.Longitude = 0;
            }
            return address;
        }

        public static OwnedAddress ConvertCoordinates(OwnedAddress Ownedaddress, string latitude, string longitude)
        {
            try
            {
                if (latitude != null &&
                    longitude != null)
                {
                    Ownedaddress.Latitude = Convert.ToDouble(latitude.Replace('.', ','));
                    Ownedaddress.Longitude = Convert.ToDouble(longitude.Replace('.', ','));
                }
            }
            catch
            {
                Ownedaddress.Latitude = 0;
                Ownedaddress.Longitude = 0;
            }
            return Ownedaddress;
        }

    }
}