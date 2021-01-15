using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeMapServer.Models;

namespace CoffeeMapServer.Services.Interfaces.Admin
{
    public interface IAddressService
    {
        public Task<int> AddAddressAsync(Address entity);

        public Task<Address> GetSingleAddressByIdAsync(Guid id);

        public Task<int> UpdateAddressAsync(Address entity);

        public Task<int> DeleteAddressAsync(Guid id);

        public Task<IList<Address>> FetchAddressesAsync();
    }
}