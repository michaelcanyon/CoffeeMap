using CoffeeMapServer.Models;
using CoffeeMapServer.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoffeeMapServer.Services.Interfaces
{
    public interface IRoasterService
    {
        public Task<IList<Roaster>> GetRoastersAsync();
        public Task<RoasterInfoViewModel> GetRoasterViewModel(Guid id);
        public Task SendRoasterRequest(RoasterRequest roasterRequest);
    }
}