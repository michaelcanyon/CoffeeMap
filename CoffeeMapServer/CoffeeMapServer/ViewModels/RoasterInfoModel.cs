using CoffeeMapServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeMapServer.ViewModels
{
    public class RoasterInfoModel
    {
        public Roaster roaster { get; set; }
        public Address address { get; set; }
        public List<Tag> tags { get; set; }
        public RoasterInfoModel()
        {
            roaster = new Roaster();
            address = new Address();
            tags = new List<Tag>();
        }
    }
}
