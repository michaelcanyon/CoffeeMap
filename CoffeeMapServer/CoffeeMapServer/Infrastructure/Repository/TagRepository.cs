using System;
using System.Collections;
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
    public class TagRepository : ITagRepository
    {
        private readonly CoffeeDbContext Context;

        public TagRepository(CoffeeDbContext context)
            => Context = context ?? throw new ArgumentNullException(nameof(CoffeeDbContext));

        public void Add(Tag entity)
            => Context.Tags.Add(entity);

        public void Delete(Tag entity)
            => Context.Tags.Remove(entity);

        public async Task<Tag> GetSingleAsync(Guid id,
                                              [CallerMemberName] string methodName = "")
            => await Context.Tags
               .TagWith($"{nameof(TagRepository)}.{methodName} ({id})")
               .FirstOrDefaultAsync(node => node.Id == id);

        public void Update(Tag entity)
            => Context.Tags.Update(entity);

        public async Task<IList<Tag>> GetListAsync([CallerMemberName] string methodName = "")
            => await Context.Tags
               .TagWith($"{nameof(TagRepository)}.{methodName}")
               .Include(t=>t.RoasterTags)
               .ToListAsync();

        public async Task<Tag> GetSingleAsync(string title,
                                              [CallerMemberName] string methodName = "")
            => await Context.Tags
               .TagWith($"{nameof(TagRepository)}.{methodName} ({title})")
               .FirstOrDefaultAsync(node => node.TagTitle == title);

        public async Task<Tag> GetSingleAsNoTrackingAsync(string title,
                                                          [CallerMemberName] string methodName = "")
            => await Context.Tags
               .AsNoTracking()
               .TagWith($"{nameof(TagRepository)}.{methodName} ({title}) No Tracking")
               .FirstOrDefaultAsync(node => node.TagTitle == title);

        public async Task SaveChangesAsync()
            => await Context.SaveChangesAsync();

        public async Task<IList<Tag>> GetTagsByTagIds(IList tagsIds,
                                                      [CallerMemberName] string methodName = "")
            => await Context.Tags
               .Where(t => tagsIds.Contains(t.Id))
               .TagWith($"{nameof(TagRepository)}.{methodName}")
               .ToListAsync();
    }
}