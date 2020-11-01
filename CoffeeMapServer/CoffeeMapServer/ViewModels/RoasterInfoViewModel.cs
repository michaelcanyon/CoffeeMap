using CoffeeMapServer.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace CoffeeMapServer.ViewModels
{
    public class RoasterInfoViewModel
    {
        public Roaster Roaster { get; set; }
       
        public Address Address { get; set; }
        
        public List<Tag> Tags { get; set; }

        public RoasterInfoViewModel(Roaster roaster, Address address, List<Tag> tagsList)
        {
            Roaster = roaster;
            Address = address;
            Tags = tagsList;
        }
    }
}
