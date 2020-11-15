using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeMapServer.Models;

namespace CoffeeMapServer.Services.Interfaces.Admin
{
    public interface IRoasterAddressConnectionService
    {
        public Task<IList<Address>> FetchAddressesAsync();

        public Task<IList<Roaster>> FetchRoastersAsync();

        public Task<Address> FetchSingleAddressByIdAsync(Guid id);

        public Task<Roaster> FetchSingleRoasterByIdAsync(Guid id);

        public Task UpdateRoasterAsync(Roaster entity);
    }
}