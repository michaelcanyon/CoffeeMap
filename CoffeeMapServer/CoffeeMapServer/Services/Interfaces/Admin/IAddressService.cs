using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeMapServer.Models;

namespace CoffeeMapServer.Services.Interfaces.Admin
{
    public interface IAddressService
    {
        public Task AddAddressAsync(Address entity);

        public Task<Address> GetSingleAddressByIdAsync(Guid id);

        public Task UpdateAddressAsync(Address entity);

        public Task DeleteAddressAsync(Guid id);

        public Task<IList<Address>> FetchAddressesAsync();
    }
}
