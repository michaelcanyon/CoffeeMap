using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeMapServer.ViewModels.DTO
{
    public class AddressD
    {
        public Guid Id { get; set; }
        public string AddressStr { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string OpeningHours { get; set; }
    }
}
