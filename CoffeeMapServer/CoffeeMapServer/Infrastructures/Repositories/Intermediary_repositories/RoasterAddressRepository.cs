using CoffeeMapServer.EF;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using CoffeeMapServer.Models.Intermediary_models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeMapServer.Infrastructures.Repositories.Intermediary_repositories
{
    public class RoasterAddressRepository : IRoasterAddressRepository
    {
        private readonly CoffeeDbContext context;
        public RoasterAddressRepository(CoffeeDbContext dbContext)
        {
            context = dbContext;
        }
        public async Task Create(RoasterAddress entity)
        {
            await context.RoasterAddresses.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            SqlParameter paramid = new SqlParameter("@id", id);
            await context.Database.ExecuteSqlRawAsync("DELETE FROM RoasterAddresses WHERE Id=@id", paramid);
            await context.SaveChangesAsync();
        }

        public async Task<List<RoasterAddress>> GetList()
        {
            return await context.RoasterAddresses.ToListAsync();
        }

        public async Task<RoasterAddress> GetSingle(int id)
        {
            SqlParameter paramid = new SqlParameter("@id", id);
            var RstrAddr= await context.RoasterAddresses.FromSqlRaw("SELECT * FROM RoasterAddresses WHERE Id=@id", paramid).ToListAsync();
            return RstrAddr.Count() > 0 ? RstrAddr.First() : null;
        }

        public async Task Update(RoasterAddress entity)
        {
            SqlParameter paramid = new SqlParameter("@id", entity.Id);
            SqlParameter address = new SqlParameter("@address", entity.AddressId);
            SqlParameter roaster = new SqlParameter("@roaster", entity.RoasterId);
            await context.Database.ExecuteSqlRawAsync("UPDATE RoasterAddresses SET AddressId=@address, RoasterId=@roaster WHERE Id=@id"
                , paramid, address, roaster);
            await context.SaveChangesAsync();
        }
    }
}
