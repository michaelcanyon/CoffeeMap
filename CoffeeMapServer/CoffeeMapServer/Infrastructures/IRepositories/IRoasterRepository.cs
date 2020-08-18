using CoffeeMapServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeMapServer.Infrastructures.IRepositories
{
    public interface IRoasterRepository: IBaseRepository<Roaster>
    {
        public Task<Roaster> GetSingleByAddressId(int addressId);
        public Task<Roaster> GetRoaster(Roaster roaster);
        public Task<Roaster> GetRoasterByName(string name);
    }
}
