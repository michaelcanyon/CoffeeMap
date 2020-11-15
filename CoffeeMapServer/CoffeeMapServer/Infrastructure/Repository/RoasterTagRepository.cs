using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IList<RoasterTag>> GetListAsync()
            => await Context.RoasterTags.ToListAsync();

        public async Task<IList<RoasterTag>> GetPairsByRoasterIdAsync(Guid roasterId)
            => await Context.RoasterTags.Where(node => node.RoasterId == roasterId).ToListAsync();

        public async Task<IList<RoasterTag>> GetPairsByTagIdAsync(Guid id)
            => await Context.RoasterTags.Where(node => node.TagId == id).ToListAsync();

        public async Task SaveChangesAsync()
            => await Context.SaveChangesAsync();

        public void DeleteRoasterTags(IList<RoasterTag> range)
            => Context.RemoveRange(range);
    }
}