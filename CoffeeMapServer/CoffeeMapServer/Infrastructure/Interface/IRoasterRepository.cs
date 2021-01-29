using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeMapServer.Models;

namespace CoffeeMapServer.Infrastructures.IRepositories
{
    public interface IRoasterRepository : IBaseRepository<Roaster>
    {
        public Task<IList<Roaster>> FetchRoastersByAddressIdAsync(Guid addressId);

        public Task<Roaster> GetRoasterByNameAsync(string name);

        public Task<Roaster> GetRoasterByNameNonTrackableAsync(string roasterName);
    }
}