using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CoffeeMapServer.Models;

namespace CoffeeMapServer.Infrastructures.IRepositories
{
    public interface IRoasterTagRepository
    {
        public Task<IList<RoasterTag>> GetPairsByRoasterIdAsNoTrackingAsync(Guid roasterId,
                                                                [CallerMemberName] string methodName = "");

        public Task<IList<RoasterTag>> GetPairsByTagIdAsync(Guid tagId,
                                                            [CallerMemberName] string methodName = "");

        public void Delete(RoasterTag entity);

        public void DeleteRoasterTags(IList<RoasterTag> range);

        public void Add(RoasterTag entity);

        public Task<IList<RoasterTag>> GetListAsync([CallerMemberName] string methodName = "");

        public Task SaveChangesAsync();
    }
}