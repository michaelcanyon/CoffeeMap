using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CoffeeMapServer.Models;

namespace CoffeeMapServer.Infrastructures.IRepositories
{
    public interface IRoasterRepository : IBaseRepository<Roaster>
    {
        public Task<IList<Roaster>> FetchRoastersByAddressIdAsync(Guid addressId,
                                                                  [CallerMemberName] string methodName = "");

        public Task<Roaster> GetRoasterByNameAsync(string name,
                                                   [CallerMemberName] string methodName = "");

        public Task<Roaster> GetRoasterByNameNonTrackableAsync(string roasterName,
                                                               [CallerMemberName] string methodName = "");
    }
}