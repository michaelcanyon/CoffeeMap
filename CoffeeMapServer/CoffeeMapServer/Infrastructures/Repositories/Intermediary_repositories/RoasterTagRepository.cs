using CoffeeMapServer.EF;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models.Intermediary_models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeMapServer.Infrastructures.Repositories.Intermediary_repositories
{
    public class RoasterTagRepository : IRoasterTagRepository
    {
        private readonly CoffeeDbContext Context;

        public RoasterTagRepository(CoffeeDbContext dbContext)
        {
            Context = dbContext;
        }

        public async Task Create(RoasterTag entity)
        {
            await Context.RoasterTags.AddAsync(entity);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var roasterTagNode = (await Context.RoasterTags.Where(node => node.Id == id).ToListAsync()).First();
            Context.RoasterTags.Remove(roasterTagNode);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(Guid roasterId, Guid TagId)
        {
            var roasterTagNode = (await Context.RoasterTags.Where(node => (node.RoasterId == roasterId && node.TagId == TagId)).ToListAsync()).First();
            Context.Remove(roasterTagNode);
            await Context.SaveChangesAsync();
        }
        public async Task<List<RoasterTag>> GetList()
        {
            var roasterTags = await Context.RoasterTags.ToListAsync();
            return roasterTags.Count() > 0 ? roasterTags : null;
        }

        public async Task<RoasterTag> GetSingle(Guid id)
        {
            var roasterTags = await Context.RoasterTags.Where(node => node.Id == id).ToListAsync();
            return roasterTags.Count() > 0 ? roasterTags.First() : null;
        }

        public async Task Update(RoasterTag entity)
        {
            Context.RoasterTags.Update(entity);
            await Context.SaveChangesAsync();
        }

        public async Task<List<RoasterTag>> GetPairsByRoasterId(Guid roasterId)
        {
            var roasterTags = await Context.RoasterTags.Where(node => node.RoasterId == roasterId).ToListAsync();
            return roasterTags.Count() > 0 ? roasterTags : null;
        }
        public async Task<List<RoasterTag>> GetPairsByTagId(Guid id)
        {
            var roasterTags = await Context.RoasterTags.Where(node => node.TagId == id).ToListAsync();
            return roasterTags.Count() > 0 ? roasterTags : null;
        }

    }
}
