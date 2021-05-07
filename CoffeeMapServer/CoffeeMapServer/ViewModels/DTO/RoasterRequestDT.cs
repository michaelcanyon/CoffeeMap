using System.Collections.Generic;

namespace CoffeeMapServer.ViewModels.DTO
{
    public class RoasterRequestDT
    {

        public OwnerDT OwnerDT { get; set; }

        public RoasterDT RoasterDT { get; set; }

        public AddressDT AddressDT { get; set; }

        public List<TagDT> Tags { get; set; }

        public string CharPicture { get; set; }

        public static RoasterRequestDT New(OwnerDT ownerDT,
                                           RoasterDT roasterDT,
                                           AddressDT addressDT,
                                           List<TagDT> tags)
            => new RoasterRequestDT()
            {
                OwnerDT=ownerDT,
                RoasterDT = roasterDT,
                AddressDT = addressDT,
                Tags = tags
            };
    }
}
