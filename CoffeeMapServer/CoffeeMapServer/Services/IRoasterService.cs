using CoffeeMapServer.Models;
using CoffeeMapServer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeMapServer.Services
{
   public interface IRoasterService
    {
        public Task<List<Roaster>> GetRoasters();
        public Task<RoasterInfoModel> GetSingleRoaster(int id);
        public Task PostRoasterRequest(RoasterRequest roasterRequest);
    }
}
