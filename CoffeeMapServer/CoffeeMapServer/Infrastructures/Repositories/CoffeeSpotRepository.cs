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
    public class CoffeeSpotRepository: ICoffeeSpotRepository
    {
        CoffeeDbContext DbContext { get; set; }
        public CoffeeSpotRepository(CoffeeDbContext context)
        {
            DbContext = context;
        }
        public async Task Create(CoffeeSpot entity)
        {
            await DbContext.CoffeeSpots.AddAsync(entity);
            await DbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            SqlParameter paramid = new SqlParameter("@id", id);
            await DbContext.Database.ExecuteSqlRawAsync("DELETE FROM CoffeeSpots WHERE Id=@id", paramid);
            await DbContext.SaveChangesAsync();
        }

        public async Task<CoffeeSpot> GetSingle(int id)
        {
            SqlParameter paramid = new SqlParameter("@id", id);
            var list = await DbContext.CoffeeSpots.FromSqlRaw("SELECT * FROM CoffeeSpots WHERE Id=@id", paramid).ToListAsync();
            return list.Count() > 0 ? list.First() : null;
        }

        public async Task Update(CoffeeSpot entity)
        {
            SqlParameter paramid = new SqlParameter("@id", entity.Id);
            SqlParameter description = new SqlParameter("@description", entity.Description);
            SqlParameter indexId = new SqlParameter("@indexId", entity.IndexId);
            SqlParameter title = new SqlParameter("@title", entity.Title);
            SqlParameter picture = new SqlParameter("@picture", entity.Picture);
            await DbContext.Database.ExecuteSqlRawAsync("UPDATE CoffeeSpots SET Description=@description, IndexId=@indexId, Title=@title, Picture=@picture  WHERE Id=@id"
                , description, indexId, title, picture);
            await DbContext.SaveChangesAsync();

        }
        public async Task<List<CoffeeSpot>> GetList()
        {
            return await DbContext.CoffeeSpots.ToListAsync();
        }
    }
}
