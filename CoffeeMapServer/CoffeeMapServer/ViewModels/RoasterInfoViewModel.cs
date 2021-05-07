using System.Collections.Generic;
using CoffeeMapServer.ViewModels.DTO;

namespace CoffeeMapServer.ViewModels
{
    public class RoasterInfoViewModel
    {
        public RoasterDT Roaster { get; set; }

        public AddressDT Address { get; set; }

        public List<TagDT> Tags { get; set; }

        public RoasterInfoViewModel(
            RoasterDT roaster,
            AddressDT address,
            List<TagDT> tagsList)
        {
            Roaster = roaster;
            Address = address;
            Tags = tagsList;
        }
        public RoasterInfoViewModel()
        { }
    }
}