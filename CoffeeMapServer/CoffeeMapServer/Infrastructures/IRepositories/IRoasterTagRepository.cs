using CoffeeMapServer.Models;
using CoffeeMapServer.Models.Intermediary_models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeMapServer.Infrastructures.IRepositories
{
   public interface IRoasterTagRepository: IBaseRepository<RoasterTag>
    {
        public Task<List<RoasterTag>> GetPairsByRoasterId(int roasterId);
        public Task Delete(int roasterId, int tagId);
    }
}
