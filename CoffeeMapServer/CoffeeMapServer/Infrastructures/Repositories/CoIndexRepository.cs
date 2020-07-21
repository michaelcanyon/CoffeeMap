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
    public class CoIndexRepository : ICoIndexRepository
    {
        CoffeeDbContext DbContext { get; set; }
        public CoIndexRepository(CoffeeDbContext context)
        {
            DbContext = context;
        }
        public async Task Create(CoIndex entity)
        {
            await DbContext.CoIndexes.AddAsync(entity);
            await DbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            SqlParameter paramid = new SqlParameter("@id", id);
            await DbContext.Database.ExecuteSqlRawAsync("DELETE FROM CoIndexes WHERE Id=@id", paramid);
            await DbContext.SaveChangesAsync();
        }

        public async Task<CoIndex> GetSingle(int id)
        {
            SqlParameter paramid = new SqlParameter("@id", id);
            var list = await DbContext.CoIndexes.FromSqlRaw("SELECT * FROM CoIndexes WHERE Id=@id", paramid).ToListAsync();
            return list.Count() > 0 ? list.First() : null;
        }

        public async Task Update(CoIndex entity)
        {
            SqlParameter paramid = new SqlParameter("@id", entity.Id);
            SqlParameter capuccino = new SqlParameter("@capuccino", entity.Capuccino);
            SqlParameter espresso = new SqlParameter("@espresso", entity.Espresso);
            await DbContext.Database.ExecuteSqlRawAsync("UPDATE CoIndexes SET Capuccino=@capuccino, Espresso=@espresso WHERE Id=@id"
                , paramid, capuccino, espresso);
            await DbContext.SaveChangesAsync();

        }
        public async Task<List<CoIndex>> GetList()
        {
            return await DbContext.CoIndexes.ToListAsync();
        }
    }
}
