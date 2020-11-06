using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeMapServer.Models;

namespace CoffeeMapServer.Services.Interfaces.Admin
{
   public interface IRoasterRequestService
    {
        public Task<IList<RoasterRequest>> FetchRoasterRequestsListAsync();

        public Task<RoasterRequest> FetchSingleRoasterRequestByIdAsync(Guid id);

        public Task UpdateRoasterRequestAsync(RoasterRequest entity);

        public Task DeleteRoasterRequestAsync(Guid id);

        public Task BindToRoasterNdAddressAsync(Guid id);

        public Task DeleteAllRoasterRequestsAsync();


    }
}
