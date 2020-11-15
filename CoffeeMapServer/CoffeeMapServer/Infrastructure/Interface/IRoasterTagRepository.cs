using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeMapServer.Models;

namespace CoffeeMapServer.Infrastructures.IRepositories
{
    public interface IRoasterTagRepository
    {
        public Task<IList<RoasterTag>> GetPairsByRoasterIdAsync(Guid roasterId);

        public Task<IList<RoasterTag>> GetPairsByTagIdAsync(Guid tagId);

        public void Delete(RoasterTag entity);

        public void DeleteRoasterTags(IList<RoasterTag> range);

        public void Add(RoasterTag entity);

        public Task<IList<RoasterTag>> GetListAsync();

        public Task SaveChangesAsync();
    }
}