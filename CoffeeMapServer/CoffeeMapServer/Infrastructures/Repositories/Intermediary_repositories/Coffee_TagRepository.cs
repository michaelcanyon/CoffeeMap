using CoffeeMapServer.EF;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models.Intermediary_models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeMapServer.Infrastructures.Repositories.Intermediary_repositories
{
    public class Coffee_TagRepository: ICoffee_TagRepository
    {
        CoffeeDbContext DbContext { get; set; }
        public Coffee_TagRepository(CoffeeDbContext context)
        {
            DbContext = context;


        }
        public async Task Create(Coffee_Tag entity)
        {
            await DbContext.coffee_Tags.AddAsync(entity);
            await DbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            SqlParameter paramid = new SqlParameter("@id", id);
            await DbContext.Database.ExecuteSqlRawAsync("DELETE FROM coffee_Tags WHERE Id=@id", paramid);
            await DbContext.SaveChangesAsync();
        }

        public async Task<Coffee_Tag> GetSingle(int id)
        {
            SqlParameter paramid = new SqlParameter("@id", id);
            var list = await DbContext.coffee_Tags.FromSqlRaw("SELECT * FROM coffee_Tags WHERE Id=@id", paramid).ToListAsync();
            return list.Count() > 0 ? list.First() : null;
        }

        public async Task Update(Coffee_Tag entity)
        {
            SqlParameter paramid = new SqlParameter("@id", entity.Id);
            SqlParameter tagId = new SqlParameter("@tagId", entity.TagId);
            SqlParameter coffeeNodeId = new SqlParameter("@coffeeNodeId", entity.Cofee_NodeId);
            await DbContext.Database.ExecuteSqlRawAsync("UPDATE coffee_Tags SET TagId=@tagId, Cofee_NodeId=@coffeeNodeId WHERE Id=@id"
                , paramid, tagId, coffeeNodeId);
            await DbContext.SaveChangesAsync();
        }

        public async Task<List<Coffee_Tag>> GetList()
        {
            return await DbContext.coffee_Tags.ToListAsync();
        }
    }
}
