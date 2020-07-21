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
    public class Coffee_RoasterRepository: ICoffee_RoasterRepository
    {
        CoffeeDbContext DbContext { get; set; }
        public Coffee_RoasterRepository(CoffeeDbContext context)
        {
            DbContext = context;
            

        }
        public async Task Create(Coffee_Roaster entity)
        {
            await DbContext.Coffee_Roasters.AddAsync(entity);
            await DbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            SqlParameter paramid = new SqlParameter("@id", id);
            await DbContext.Database.ExecuteSqlRawAsync("DELETE FROM Coffee_Roasters WHERE Id=@id", paramid);
            await DbContext.SaveChangesAsync();
        }

        public async Task<Coffee_Roaster> GetSingle(int id)
        {
            SqlParameter paramid = new SqlParameter("@id", id);
            var list = await DbContext.Coffee_Roasters.FromSqlRaw("SELECT * FROM Coffee_Roasters WHERE Id=@id", paramid).ToListAsync();
            return list.Count() > 0 ? list.First() : null;
        }

        public async Task Update(Coffee_Roaster entity)
        {
            SqlParameter paramid = new SqlParameter("@id", entity.Id);
            SqlParameter roasterId = new SqlParameter("@roasterId", entity.RoasterId);
            SqlParameter coffeeNodeId = new SqlParameter("@coffeeNodeId", entity.Cofee_NodeId);
            await DbContext.Database.ExecuteSqlRawAsync("UPDATE Coffee_Roasters SET RoasterId=@roasterId, Cofee_NodeId=@coffeeNodeId WHERE Id=@id"
                , paramid, roasterId, coffeeNodeId);
            await DbContext.SaveChangesAsync();
        }

        public async Task<List<Coffee_Roaster>> GetList()
        {
            return await DbContext.Coffee_Roasters.ToListAsync();
        }
    }
}
