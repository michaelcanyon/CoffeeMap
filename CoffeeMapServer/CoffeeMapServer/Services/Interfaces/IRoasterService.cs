using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeMapServer.ViewModels;

namespace CoffeeMapServer.Services.Interfaces
{
    public interface IRoasterService
    {
        public Task<IList<RoasterInfoViewModel>> GetRoastersAsync();
        public Task<RoasterInfoViewModel> GetRoasterViewModel(Guid id);
    }
}