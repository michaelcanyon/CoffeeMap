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
    public class Coffee_addressRepository : ICoffee_AddressRepository
    {
        CoffeeDbContext DbContext { get; set; }
        public Coffee_addressRepository(CoffeeDbContext context)
        {
            DbContext = context;
        }
        public async Task Create(Coffee_Address entity)
        {
            await DbContext.Coffee_Addresses.AddAsync(entity);
            await DbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            SqlParameter paramid = new SqlParameter("@id", id);
            await DbContext.Database.ExecuteSqlRawAsync("DELETE FROM Coffee_Addresses WHERE Id=@id", paramid);
            await DbContext.SaveChangesAsync();
        }

        public async Task<Coffee_Address> GetSingle(int id)
        {
            SqlParameter paramid = new SqlParameter("@id", id);
            var list = await DbContext.Coffee_Addresses.FromSqlRaw("SELECT * FROM Coffee_Addresses WHERE Id=@id", paramid).ToListAsync();
            return list.Count() > 0 ? list.First() : null;
        }

        public async Task Update(Coffee_Address entity)
        {
            SqlParameter paramid = new SqlParameter("@id", entity.Id);
            SqlParameter addressId = new SqlParameter("@addressId", entity.AddressId);
            SqlParameter coffeeNodeId = new SqlParameter("@coffeeNodeId", entity.Cofee_NodeId);
            await DbContext.Database.ExecuteSqlRawAsync("UPDATE Coffee_Addresses SET AddressId=@addressId, Cofee_NodeId=@coffeeNodeId WHERE Id=@id"
                , paramid, addressId, coffeeNodeId);
            await DbContext.SaveChangesAsync();

        }

        public async Task<List<Coffee_Address>> GetList()
        {
            return await DbContext.Coffee_Addresses.ToListAsync();
        }
    }
}
