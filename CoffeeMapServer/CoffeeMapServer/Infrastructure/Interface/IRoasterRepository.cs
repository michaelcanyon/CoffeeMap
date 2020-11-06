using CoffeeMapServer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoffeeMapServer.Infrastructures.IRepositories
{
    public interface IRoasterRepository : IBaseRepository<Roaster>
    {
        public Task<Roaster> GetSingleByAddressIdAsync(Guid addressId);

        public Task<IList<Roaster>> FetchRoastersByAddressIdAsync(Guid addressId);
       
        public Task<Roaster> GetRoasterAsync(Roaster roaster);
        
        public Task<Roaster> GetRoasterByNameAsync(string name);
    }
}
