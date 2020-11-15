using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeMapServer.EF;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeMapServer.Infrastructures.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly CoffeeDbContext Context;

        public TagRepository(CoffeeDbContext context)
            => Context = context ?? throw new ArgumentNullException(nameof(CoffeeDbContext));

        public void Add(Tag entity)
            => Context.Tags.Add(entity);

        public void Delete(Tag entity)
            => Context.Tags.Remove(entity);

        public async Task<Tag> GetSingleAsync(Guid id)
            => await Context.Tags.FirstOrDefaultAsync(node => node.Id == id);

        public void Update(Tag entity)
            => Context.Tags.Update(entity);

        public async Task<IList<Tag>> GetListAsync()
            => await Context.Tags.ToListAsync();

        public async Task<Tag> GetSingleAsync(string title)
            => await Context.Tags.FirstOrDefaultAsync(node => node.TagTitle == title);

        public async Task SaveChangesAsync()
            => await Context.SaveChangesAsync();
    }
}