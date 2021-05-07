using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeMapServer.Models;
using Microsoft.AspNetCore.Http;

namespace CoffeeMapServer.Services.Interfaces.Admin
{
    public interface IRoasterAdminService
    {
        public Task<IList<Roaster>> FetchRoastersAsync();

        public Task<Roaster> FetchSingleRoasterAsync(Guid id);

        public Task<int> UpdateRoasterAsync(Roaster entity,
                                            string tags,
                                            string latitude,
                                            string longitude,
                                            IFormFile picture);

        public Task<int> AddRoasterAsync(Roaster roaster,
                                         string tags,
                                         Address address,
                                         string latitude,
                                         string longitude,
                                         IFormFile picture);

        public Task<int> DeleteRoasterByIdAsync(Guid id);
    }
}