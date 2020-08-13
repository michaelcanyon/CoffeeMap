using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeMapServer.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace CoffeeMapServer.Infrastructures.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly CoffeeDbContext DbContext;
        public TagRepository(CoffeeDbContext context)
        {
            DbContext = context;
        }
        public async Task Create(Tag entity)
        {
            await DbContext.Tags.AddAsync(entity);
            await DbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            SqlParameter paramid = new SqlParameter("@id", id);
            await DbContext.Database.ExecuteSqlRawAsync("DELETE FROM Tags WHERE Id=@id", paramid);
            await DbContext.SaveChangesAsync();
        }

        public async Task<Tag> GetSingle(int id)
        {
            SqlParameter paramid = new SqlParameter("@id", id);
            var list = await DbContext.Tags.FromSqlRaw("SELECT * FROM Tags WHERE Id=@id", paramid).ToListAsync();
            return list.Count() > 0 ? list.First() : null;
        }

        public async Task Update(Tag entity)
        {
            SqlParameter paramid = new SqlParameter("@id", entity.Id);
            SqlParameter tagTitle = new SqlParameter("@tagTitle", entity.TagTitle);
            await DbContext.Database.ExecuteSqlRawAsync("UPDATE Tags SET TagTitle=@tagTitle WHERE Id=@id"
                , paramid, tagTitle);
            await DbContext.SaveChangesAsync();
        }

        public async Task<List<Tag>> GetList()
        {
            return await DbContext.Tags.ToListAsync();
        }
        public async Task<Tag> GetSingle(string title) 
        {
            SqlParameter tagTitle = new SqlParameter("@tagTitle", title);
            var tags = await DbContext.Tags.FromSqlRaw("SELECT * FROM Tags WHERE TagTitle=@tagTitle").ToListAsync();
            return tags.Count() >= 1 ? tags.First() : null;
        }
    }
}
