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
    public class RoasterRepository: IRoasterRepository
    {
        CoffeeDbContext DbContext { get; set; }
        public RoasterRepository(CoffeeDbContext context)
        {
            DbContext = context;
        }
        public async Task Create(Roaster entity)
        {
            await DbContext.Roasters.AddAsync(entity);
            await DbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            SqlParameter paramid = new SqlParameter("@id", id);
            await DbContext.Database.ExecuteSqlRawAsync("DELETE FROM Roasters WHERE Id=@id", paramid);
            await DbContext.SaveChangesAsync();
        }

        public async Task<Roaster> GetSingle(int id)
        {
            SqlParameter paramid = new SqlParameter("@id", id);
            var list = await DbContext.Roasters.FromSqlRaw("SELECT * FROM Roasters WHERE Id=@id", paramid).ToListAsync();
            return list.Count() > 0 ? list.First() : null;
        }

        public async Task Update(Roaster entity)
        {
            SqlParameter paramid = new SqlParameter("@id", entity.Id);
            SqlParameter name = new SqlParameter("@name", entity.Name);
            await DbContext.Database.ExecuteSqlRawAsync("UPDATE Roasters SET Name=@name WHERE Id=@id"
                , paramid, name);
            await DbContext.SaveChangesAsync();

        }
        public async Task<List<Roaster>> GetList()
        {
            return await DbContext.Roasters.ToListAsync();
        }
    }
}
