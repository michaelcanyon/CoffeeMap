using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CoffeeMapServer.EF;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeMapServer.Infrastructures.Repositories
{
    public class RoasterTagRepository : IRoasterTagRepository
    {
        private readonly CoffeeDbContext Context;

        public RoasterTagRepository(CoffeeDbContext dbContext)
            => Context = dbContext ?? throw new ArgumentNullException(nameof(CoffeeDbContext));

        public void Add(RoasterTag entity)
            => Context.RoasterTags.Add(entity);

        public void Delete(RoasterTag entity)
            => Context.Remove(entity);

        public async Task<IList<RoasterTag>> GetListAsync([CallerMemberName] string methodName = "")
            => await Context.RoasterTags
               .TagWith($"{nameof(RoasterTagRepository)}.{methodName}")
               .ToListAsync();

        public async Task<IList<RoasterTag>> GetPairsByRoasterIdAsync(Guid roasterId,
                                                                      [CallerMemberName] string methodName = "")
            => await Context.RoasterTags
                .Where(node => node.RoasterId == roasterId)
                .TagWith($"{nameof(RoasterTagRepository)}.{methodName} ({roasterId})")
                .ToListAsync();

        public async Task<IList<RoasterTag>> GetPairsByTagIdAsync(Guid id,
                                                                  [CallerMemberName] string methodName = "")
            => await Context.RoasterTags
                .Where(node => node.TagId == id)
                .TagWith($"{nameof(RoasterTagRepository)}.{methodName} ({id})")
                .ToListAsync();

        public async Task SaveChangesAsync()
            => await Context.SaveChangesAsync();

        public void DeleteRoasterTags(IList<RoasterTag> range)
            => Context.RemoveRange(range);
    }
}