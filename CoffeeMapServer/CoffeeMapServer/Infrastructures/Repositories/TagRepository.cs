using CoffeeMapServer.EF;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeMapServer.Infrastructures.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly CoffeeDbContext Context;

        public TagRepository(CoffeeDbContext context)
        {
            Context = context;
        }

        public async Task Create(Tag entity)
        {
            await Context.Tags.AddAsync(entity);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var tag = (await Context.Tags.Where(node => node.Id == id).ToListAsync()).First();
            Context.Tags.Remove(tag);
            await Context.SaveChangesAsync();
        }

        public async Task<Tag> GetSingle(Guid id)
        {
            var tag = await Context.Tags.Where(node => node.Id == id).ToListAsync();
            return tag.Count() > 0 ? tag.First() : null;
        }

        public async Task Update(Tag entity)
        {
            Context.Tags.Update(entity);
            await Context.SaveChangesAsync();
        }

        public async Task<List<Tag>> GetList()
        {
            var tags = await Context.Tags.ToListAsync();
            return tags.Count() > 0 ? tags : null;
        }

        public async Task<Tag> GetSingle(string title)
        {
            var tags = await Context.Tags.Where(node => node.TagTitle == title).ToListAsync();
            return tags.Count() > 0 ? tags.First() : null;
        }
    }
}
