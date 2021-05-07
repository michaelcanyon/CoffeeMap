using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeMapServer.Models;

namespace CoffeeMapServer.Services.Interfaces.Admin
{
    public interface IAddressService
    {
        public Task<int> AddAddressAsync(Address entity, string latitdue, string longitude);

        public Task<Address> GetSingleAddressByIdAsync(Guid id);

        public Task<int> UpdateAddressAsync(Address entity, string latitude, string longitude);

        public Task<int> DeleteAddressAsync(Guid id);

        public Task<IList<Address>> FetchAddressesAsync();
    }
}