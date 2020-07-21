using CoffeeMapServer.EF;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeMapServer.Infrastructures.Repositories
{
    public class TagRepository: ITagRepository
    {
        CoffeeDbContext DbContext { get; set; }
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
            SqlParameter title = new SqlParameter("@title", entity.Title);
            await DbContext.Database.ExecuteSqlRawAsync("UPDATE Tags SET Title=@title WHERE Id=@id"
                , paramid, title);
            await DbContext.SaveChangesAsync();

        }
        public async Task<List<Tag>> GetList()
        {
            return await DbContext.Tags.ToListAsync();
        }
    }
}
