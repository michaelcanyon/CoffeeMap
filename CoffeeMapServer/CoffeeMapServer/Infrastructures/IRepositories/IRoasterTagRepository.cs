using CoffeeMapServer.Models.Intermediary_models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoffeeMapServer.Infrastructures.IRepositories
{
    public interface IRoasterTagRepository : IBaseRepository<RoasterTag>
    {
        public Task<List<RoasterTag>> GetPairsByRoasterId(Guid roasterId);

        public Task<List<RoasterTag>> GetPairsByTagId(Guid tagId);

        public Task Delete(Guid roasterId, Guid tagId);
    }
}
