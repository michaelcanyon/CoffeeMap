using CoffeeMapServer.Models;
using CoffeeMapServer.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoffeeMapServer.Services.Interfaces
{
    public interface IRoasterService
    {
        public Task<List<Roaster>> GetRoasters();
        public Task<RoasterInfoViewModel> GetSingleRoaster(Guid id);
        public Task SendRoasterRequest(RoasterRequest roasterRequest);
    }
}
