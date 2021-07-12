using System;
using CoffeeMapServer.Models;
using CoffeeMapServer.Models.OwnedModels;

namespace CoffeeMapServer.builders
{
    public static class AddressCoordinatesTransformer
    {
        public static Address ConvertCoordinates(Address address, string latitude, string longitude)
        {
            var _address = Address.New(address.Id, address.AddressStr, address.OpeningHours ?? "none", 0, 0);
            try
            {
                //var lat = Convert.ToDouble(latitude.Replace('.', ','));
                //var lng = Convert.ToDouble(longitude.Replace('.', ','));
                var lat = Convert.ToDouble(latitude);
                var lng = Convert.ToDouble(longitude);
                _address.Latitude = lat;
                _address.Longitude = lng;
            }
            catch
            {
                _address.Latitude = 0;
                _address.Longitude = 0;
            }
            return _address;
        }

        public static OwnedAddress ConvertCoordinates(OwnedAddress Ownedaddress, string latitude, string longitude)
        {
            var _address = OwnedAddress.New(Ownedaddress.AddressStr, Ownedaddress.OpeningHours ?? "none", 0, 0);
            try
            {
                //var lat = Convert.ToDouble(latitude.Replace('.', ','));
                //var lng = Convert.ToDouble(longitude.Replace('.', ','));
                var lat = Convert.ToDouble(latitude);
                var lng = Convert.ToDouble(longitude);
                _address.Latitude = lat;
                _address.Longitude = lng;
            }
            catch
            {
                _address.Latitude = 0;
                _address.Longitude = 0;
            }
            return _address;
        }

    }
}