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
    public class Coffee_IndexRepository: ICoffee_IndexRepository
    {
        CoffeeDbContext DbContext { get; set; }
        public Coffee_IndexRepository(CoffeeDbContext context)
        {
            DbContext = context;
            
        }
        public async Task Create(Coffee_Index entity)
        {
            await DbContext.coffee_Indices.AddAsync(entity);
            await DbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            SqlParameter paramid = new SqlParameter("@id", id);
            await DbContext.Database.ExecuteSqlRawAsync("DELETE FROM coffee_Indices WHERE Id=@id", paramid);
            await DbContext.SaveChangesAsync();
        }

        public async Task<Coffee_Index> GetSingle(int id)
        {
            SqlParameter paramid = new SqlParameter("@id", id);
            var list = await DbContext.coffee_Indices.FromSqlRaw("SELECT * FROM coffee_Indices WHERE Id=@id", paramid).ToListAsync();
            return list.Count() > 0 ? list.First() : null;
        }

        public async Task Update(Coffee_Index entity)
        {
            SqlParameter paramid = new SqlParameter("@id", entity.Id);
            SqlParameter indexId = new SqlParameter("@indexId", entity.IndexId);
            SqlParameter coffeeNodeId = new SqlParameter("@coffeeNodeId", entity.Cofee_NodeId);
            await DbContext.Database.ExecuteSqlRawAsync("UPDATE coffee_Indices SET IndexId=@indexId, Cofee_NodeId=@coffeeNodeId WHERE Id=@id"
                , paramid, indexId, coffeeNodeId);
            await DbContext.SaveChangesAsync();

        }

        public async Task<List<Coffee_Index>> GetList()
        {
            return await DbContext.coffee_Indices.ToListAsync();
        }
    }
}
