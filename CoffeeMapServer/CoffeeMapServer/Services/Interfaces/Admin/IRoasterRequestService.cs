using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeMapServer.Models;

namespace CoffeeMapServer.Services.Interfaces.Admin
{
    public interface IRoasterRequestService
    {
        public Task<IList<RoasterRequest>> FetchRoasterRequestsListAsync();

        public Task<RoasterRequest> FetchSingleRoasterRequestByIdAsync(Guid id);

        public Task<int> UpdateRoasterRequestAsync(RoasterRequest entity);

        public Task<int> DeleteRoasterRequestAsync(Guid id);

        public Task<int> BindToRoasterNdAddressAsync(Guid id);

        public Task<int> DeleteAllRoasterRequestsAsync();
    }
}