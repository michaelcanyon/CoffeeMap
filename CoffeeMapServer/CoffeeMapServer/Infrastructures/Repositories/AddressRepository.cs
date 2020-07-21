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
    public class AddressRepository : IAddessRepository
    {
        CoffeeDbContext DbContext { get; set; }
        public AddressRepository(CoffeeDbContext context)
        {
            DbContext = context;
        }
        public async Task Create(Address entity)
        {
            await DbContext.Addresses.AddAsync(entity);
            await DbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            SqlParameter paramid = new SqlParameter("@id", id);
            await DbContext.Database.ExecuteSqlRawAsync("DELETE FROM Addresses WHERE Id=@id", paramid);
            await DbContext.SaveChangesAsync();
        }

        public async Task<Address> GetSingle(int id)
        {
            SqlParameter paramid = new SqlParameter("@id", id);
            var list = await DbContext.Addresses.FromSqlRaw("SELECT * FROM Addresses WHERE Id=@id", paramid).ToListAsync();
            return list.Count() > 0 ? list.First() : null;
        }

        public async Task Update(Address entity)
        {
            SqlParameter paramid = new SqlParameter("@id", entity.Id);
            SqlParameter address = new SqlParameter("@address", entity.AddressStr);
            SqlParameter hours = new SqlParameter("@hours", entity.OpeningHours);
            await DbContext.Database.ExecuteSqlRawAsync("UPDATE Addresses SET AddressStr=@address, OpeningHours=@hours WHERE Id=@id"
                , paramid, address, hours);
            await DbContext.SaveChangesAsync();

        }

        public async Task<List<Address>> GetList()
        {
            return await DbContext.Addresses.ToListAsync();
        }
    }
}
