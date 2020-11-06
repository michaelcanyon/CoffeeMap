using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeMapServer.Models.Intermediary_models;

namespace CoffeeMapServer.Infrastructures.IRepositories
{
    public interface IRoasterTagRepository
    {
        public Task<IList<RoasterTag>> GetPairsByRoasterIdAsync(Guid roasterId);

        public Task<IList<RoasterTag>> GetPairsByTagIdAsync(Guid tagId);


        //TODO: perhaps you will have o write new method & signature to get roastertag pair by roaster Id & Tag Id
        public void Delete(RoasterTag entity);

        public void Add(RoasterTag entity);

        public void Update(RoasterTag entity);

        public Task<IList<RoasterTag>> GetListAsync();

        public Task SaveChangesAsync();
    }
}
